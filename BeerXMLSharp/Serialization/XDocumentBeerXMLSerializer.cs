using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using BeerXMLSharp.OM;

namespace BeerXMLSharp.Serialization
{
    /// <summary>
    /// Implementation of IBeerXMLSerializer that uses LINQ XML
    /// serialization to serialize the objects
    /// </summary>
    internal class XDocumentBeerXMLSerializer : IBeerXMLSerializer
    {
        #region Helper class

        /// <summary>
        /// Private wrapper class to represent a property that should
        /// be included in the beer XML serialization
        /// </summary>
        private class BeerXMLProperty
        {
            /// <summary>
            /// Upper case name of the property
            /// </summary>
            public string Name
            {
                get
                {
                    return this.Property.Name.ToUpperInvariant();
                }
            }

            /// <summary>
            /// Indicates if this property is of type IBeerXMLEntity.
            /// </summary>
            /// <value>
            ///   <c>true</c> if this instance is IBeerXMLEntity; otherwise, <c>false</c>.
            /// </value>
            public bool IsIBeerXMLEntity
            {
                get;
            }

            /// <summary>
            /// Reflection PropertyInfo representing the property
            /// </summary>
            public PropertyInfo Property
            {
                get;
            }

            /// <summary>
            /// The BeerXMLIncludeAttribute that was used on this property
            /// </summary>
            public BeerXMLIncludeAttribute Attribute
            {
                get;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="BeerXMLProperty"/> class.
            /// </summary>
            /// <param name="property">The property.</param>
            /// <param name="attribute">The attribute.</param>
            /// <param name="isIBeerXMLEntity">if set to <c>true</c> [is i beer XML entity].</param>
            public BeerXMLProperty(PropertyInfo property, BeerXMLIncludeAttribute attribute, bool isIBeerXMLEntity)
            {
                this.Property = property;
                this.Attribute = attribute;
                this.IsIBeerXMLEntity = isIBeerXMLEntity;
            }
        }

        #endregion

        #region Static initialization

        /// <summary>
        /// Static dictionary used to map types to property info, to avoid some extra reflection calls
        /// at serialization time (do it all up front)
        /// </summary>
        private static readonly IDictionary<Type, IDictionary<string, BeerXMLProperty>> _typeToPropertyMap = new Dictionary<Type, IDictionary<string, BeerXMLProperty>>();

        private static readonly IDictionary<string, Type> _typeNameToTypeMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Static constructor builds the dictionary of Types to property information
        /// </summary>
        static XDocumentBeerXMLSerializer()
        {
            // get the assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // find every IBeerXMLEntity type in the assembly that is a non-abstract class
            foreach (Type type in assembly.GetTypes().Where(t => typeof(IBeerXMLEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
            {
                // add the name -> type mapping
                _typeNameToTypeMap.Add(type.Name, type);

                IDictionary<string, BeerXMLProperty> propInfoList = new Dictionary<string, BeerXMLProperty>(StringComparer.OrdinalIgnoreCase);

                // find every property that has the BeerXMLIncludeAttribute on it
                foreach (PropertyInfo property in type.GetProperties())
                {
                    BeerXMLIncludeAttribute attribute = property.GetCustomAttribute(typeof(BeerXMLIncludeAttribute), inherit: true) as BeerXMLIncludeAttribute;

                    if (attribute != null)
                    {
                        BeerXMLProperty prop = new BeerXMLProperty(property, attribute, typeof(IBeerXMLEntity).IsAssignableFrom(property.PropertyType));
                        propInfoList.Add(prop.Name, prop);
                    }
                }

                // add to dictionary
                _typeToPropertyMap.Add(type, propInfoList);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Serializes the given IBeerXMLEntity object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(IBeerXMLEntity obj)
        {
            XDocument document = GetBeerXDocument(obj);
            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriter(builder))
            {
                document.Save(writer);
            }

            return builder.ToString();
        }

        public IBeerXMLEntity Deserialize(string filePath)
        {
            XDocument document = GetXDocumentFromFile(filePath);

            return GetEntityFromElement(document.Root);
        }

        #endregion

        #region Deserialization helpers

        private IBeerXMLEntity GetEntityFromElement(XElement element)
        {
            // get the name of the child property
            string propertyName = element.Name.ToString();

            Type objectType;

            // get the property type from the property name
            if (!_typeNameToTypeMap.TryGetValue(propertyName, out objectType))
            {
                throw new ArgumentException(string.Format("Invalid type with property name [{0}]", propertyName));
            }

            // create the empty IBeerXMLEntity
            IBeerXMLEntity objectFromXElement = (IBeerXMLEntity)Activator.CreateInstance(objectType, nonPublic: true);

            if (objectFromXElement is IRecordSet)
            {
                IRecordSet objAsRecordSet = objectFromXElement as IRecordSet;

                foreach (XElement childElement in element.Elements())
                {
                    objAsRecordSet.Add(GetEntityFromElement(childElement));
                }

                return objAsRecordSet;
            }

            // get the dictionary of property name to property for this type
            IDictionary<string, BeerXMLProperty> propertyList = null;
            if (!_typeToPropertyMap.TryGetValue(objectType, out propertyList))
            {
                throw new ArgumentException(string.Format("Invalid type with name [{0}] - unable to find property list!", objectType.Name));
            }

            // foreach child element, set the property
            foreach (XElement childElement in element.Elements())
            {
                string childName = childElement.Name.ToString();

                BeerXMLProperty property = propertyList[childName];

                SetPropertyByType(property, objectFromXElement, childElement);
            }

            return objectFromXElement;
        }

        private void SetPropertyByType(BeerXMLProperty beerXMLProperty, IBeerXMLEntity entity, XElement currentXElement)
        {
            if (beerXMLProperty.IsIBeerXMLEntity)
            {
                IBeerXMLEntity childEntity = this.GetEntityFromElement(currentXElement);
                beerXMLProperty.Property.SetValue(entity, childEntity);
            }
            else
            {
                // not an IBeerXMLEntity
                beerXMLProperty.Property.SetValue(entity, ParseType(beerXMLProperty.Property.PropertyType, currentXElement.Value));
            }
        }

        private object ParseType(Type type, string value)
        {
            if (type.IsEnum)
            {
                return Enum.Parse(type, value.Replace(' ', '_'));
            }

            return Convert.ChangeType(value, type);
        }


        private XDocument GetXDocumentFromFile(string filePath)
        {
            XDocument result = null;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                result = XDocument.Load(fs);
            }

            return result;
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
            XElement element = new XElement(obj.TagName);

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
            XElement element = new XElement(obj.TagName);

            Type objType = obj.GetType();

            foreach (KeyValuePair<string, BeerXMLProperty> typeProperty in _typeToPropertyMap[objType])
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
