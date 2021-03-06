﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using BeerXMLSharp.OM;
using BeerXMLSharp.Utilities;

namespace BeerXMLSharp.Serialization
{
    /// <summary>
    /// Implementation of IBeerXMLSerializer that uses LINQ XML
    /// serialization to serialize the objects
    /// </summary>
    internal class XDocumentBeerXMLSerializer : IBeerXMLSerializer
    {
        private IStreamFactory _fileStreamFactory = null;
        internal IStreamFactory StreamFactory
        {
            get
            {
                if (_fileStreamFactory == null)
                {
                    _fileStreamFactory = new FileStreamFactory();
                }

                return _fileStreamFactory;
            }
            set
            {
                _fileStreamFactory = value;
            }
        }

        #region Public methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool StrictModeEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Serializes the specified IBeerXMLEntity to BeerXML and output
        /// to the given file.
        /// </summary>
        /// <param name="obj">The IBeerXMLEntity object.</param>
        /// <param name="filePath"></param>
        public void Serialize(IBeerXMLEntity obj, string filePath)
        {
            using (Stream fs = this.StreamFactory.GetFileStream(filePath, FileMode.OpenOrCreate))
            {
                Serialize(obj, fs);
            }
        }

        /// <summary>
        /// Serializes the specified IBeerXMLEntity to BeerXML and output
        /// to the given stream.
        /// </summary>
        /// <param name="obj">The IBeerXMLEntity object.</param>
        /// <param name="filePath"></param>
        public void Serialize(IBeerXMLEntity obj, Stream stream)
        {
            XDocument document = GetBeerXDocument(obj);

            document.Save(stream);
        }

        /// <summary>
        /// Deserializes the contents of the specified file path into
        /// and IBeerXMLEntity object
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public IBeerXMLEntity Deserialize(string filePath)
        {
            using (Stream fs = this.StreamFactory.GetFileStream(filePath, FileMode.Open))
            {
                return Deserialize(fs);
            }
        }

        /// <summary>
        /// Deserializes the contents of the specified file path into
        /// and IBeerXMLEntity object
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        public IBeerXMLEntity Deserialize(Stream stream)
        {
            XDocument document = XDocument.Load(stream);

            return GetEntityFromElement(document.Root);
        }

        #endregion

        #region Deserialization helpers

        /// <summary>
        /// Parses an IBeerXMLEntity from the given XElement
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private IBeerXMLEntity GetEntityFromElement(XElement element)
        {
            // get the name of the child property
            string propertyName = element.Name.ToString();

            Type objectType = BeerXMLProperty.TryGetTypeByName(propertyName);

            // throw if the name of this tag does not have a corresponding
            // BeerXML type
            if (objectType == null)
            {
                if (this.StrictModeEnabled)
                {
                    throw new BeerXMLUnknownTypeTagException(propertyName, string.Format("Tag with name [{0}] has no corresponding IBeerXMLEntity type!", propertyName));
                }

                return null; 
            }

            // create the empty IBeerXMLEntity
            IBeerXMLEntity objectFromXElement = (IBeerXMLEntity)Activator.CreateInstance(objectType, nonPublic: true);

            // IRecordSets need to have their child records
            // deserialized and added to the collection
            if (objectFromXElement is IRecordSet)
            {
                IRecordSet objAsRecordSet = objectFromXElement as IRecordSet;

                foreach (XElement childElement in element.Elements())
                {
                    IRecord child = GetEntityFromElement(childElement) as IRecord;

                    // could be null if it was an invalid tag.
                    // Invalid tags are ignored
                    if (child != null)
                    {
                        objAsRecordSet.Add(child);
                    }
                }

                return objAsRecordSet;
            }

            // get the dictionary of property name to property for this type
            IDictionary<string, BeerXMLProperty> propertyList = BeerXMLProperty.GetBeerXMLPropertyList(objectType);

            // foreach child element, set the property
            foreach (XElement childElement in element.Elements())
            {
                string childName = childElement.Name.ToString();

                // this means that the given tag is unrecognized - since the overall
                // property type is recognized, just ignore it
                if (!propertyList.ContainsKey(childName))
                {
                    continue;
                }

                BeerXMLProperty property = propertyList[childName];

                SetPropertyByType(property, objectFromXElement, childElement);
            }

            return objectFromXElement;
        }

        /// <summary>
        /// Sets the type of the property by.
        /// </summary>
        /// <param name="beerXMLProperty">The beer XML property.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="currentXElement">The current x element.</param>
        private void SetPropertyByType(BeerXMLProperty beerXMLProperty, IBeerXMLEntity entity, XElement currentXElement)
        {
            if (beerXMLProperty.IsIBeerXMLEntity)
            {
                IBeerXMLEntity childEntity = this.GetEntityFromElement(currentXElement);

                // could be null if it is an invalid tag
                if (childEntity != null)
                {
                    beerXMLProperty.Property.SetValue(entity, childEntity);
                }
            }
            else
            {
                // not an IBeerXMLEntity
                beerXMLProperty.Property.SetValue(entity, ParseType(beerXMLProperty.Property.PropertyType, currentXElement.Value));
            }
        }

        /// <summary>
        /// Parses the string into the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private object ParseType(Type type, string value)
        {
            // if the type is nullable, get the underlyng type and 
            // parse into that
            Type underlyingNullable = Nullable.GetUnderlyingType(type);

            // parse using the underlying type if applicable 
            Type typeToParse = underlyingNullable == null ? type : underlyingNullable;

            // parse enums directly with spaces replacing underscores (covnention)
            if (typeToParse.IsEnum)
            {
                return Enum.Parse(typeToParse, value.Replace(' ', '_'));
            }

            // ints need to be parsed to doubles first
            // to support 
            object valueToConvert;

            if (typeToParse.IsInt32())
            {
                valueToConvert = Convert.ToDouble(value);
            }
            else
            {
                valueToConvert = value;
            }

            // convert to the target type
            return Convert.ChangeType(valueToConvert, typeToParse);
        }

        #endregion

        #region Serialization helpers

        /// <summary>
        /// Creates an XDocument from the given IBeerXMLEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private XDocument GetBeerXDocument(IBeerXMLEntity obj)
        {
            XDocument document = new XDocument(new XDeclaration(Constants.XML_VERSION, Constants.XML_ENCODING, Constants.XML_STANDALONE_YES));

            document.Add(GetBeerXElement(obj));

            return document;
        }

        /// <summary>
        /// Creates an XElement from the given IBeerXMLEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private XElement GetBeerXElement(IBeerXMLEntity obj)
        {
            XElement element;

            if (obj is IRecordSet)
            {
                element = GetBeerXElement(obj as IRecordSet);
            }
            else
            {
                element = GetBeerXElement(obj as IRecord);
            }

            return element;
        }

        /// <summary>
        /// Creates an XElement from the given IRecordSet
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private XElement GetBeerXElement(IRecordSet obj)
        {
            XElement element = new XElement(obj.GetType().Name.ToUpperInvariant());

            // get children XML
            foreach (IBeerXMLEntity record in obj)
            {
                element.Add(GetBeerXElement(record));
            }

            // force closing tag for empty elements
            if (element.IsEmpty)
            {
                element.Value = string.Empty;
            }

            return element;
        }

        /// <summary>
        /// Creates an XElement from the given IRecord
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private XElement GetBeerXElement(IRecord obj)
        {
            XElement element = new XElement(obj.GetType().Name.ToUpperInvariant());

            Type objType = obj.GetType();

            foreach (KeyValuePair<string, BeerXMLProperty> typeProperty in BeerXMLProperty.GetBeerXMLPropertyList(objType))
            {
                BeerXMLProperty beerXmlProperty = typeProperty.Value;

                string propertyName = beerXmlProperty.Name;

                object propertyValue = beerXmlProperty.Property.GetValue(obj);

                if (ShouldAddProperty(beerXmlProperty.Attribute, propertyValue))
                {
                    if (beerXmlProperty.IsIBeerXMLEntity)
                    {
                        element.Add(GetBeerXElement((IBeerXMLEntity)propertyValue));
                    }
                    else
                    {
                        element.Add(new XElement(propertyName, GetStringFromProperty(beerXmlProperty.Property.PropertyType, propertyValue)));
                    }
                }
            }

            return element;
        }

        /// <summary>
        /// Indicates whether the property value should be added to the final XElement representation
        /// based on the attribute and the value of the property
        /// </summary>
        /// <param name="beerAttribute"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private bool ShouldAddProperty(BeerXMLIncludeAttribute beerAttribute, object propertyValue)
        {
            if (beerAttribute == null)
            {
                return false;
            }

            bool propertyIsNull = propertyValue == null;

            if (beerAttribute.Requirement == PropertyRequirement.REQUIRED)
            {
                if (propertyIsNull)
                {
                    throw new InvalidOperationException("Required type with null value");
                }

                return true;
            }

            if (propertyIsNull)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Helper method to convert properties to strings based on their type
        /// </summary>
        /// <param name="propertyType"></param>
        /// <param name="propertyValue"></param>
        /// <returns></returns>
        private string GetStringFromProperty(Type propertyType, object propertyValue)
        {
            if (propertyType.IsEnum)
            {
                return EnumUtilities.ConvertEnumToString((Enum)propertyValue);
            }

            if (propertyType == typeof(bool))
            {
                return propertyValue.ToString().ToUpperInvariant();
            }

            return propertyValue.ToString();
        }

        #endregion
    }
}
