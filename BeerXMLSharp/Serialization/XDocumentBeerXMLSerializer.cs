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

        /// <summary>
        /// Static dictionary used to map types to property info, to avoid some extra reflection calls
        /// at serialization time (do it all up front)
        /// </summary>
        private static IDictionary<Type, IList<BeerXMLProperty>> _typeToPropertyMap = new Dictionary<Type, IList<BeerXMLProperty>>();

        /// <summary>
        /// Static constructor builds the dictionary of Types to property information
        /// </summary>
        static XDocumentBeerXMLSerializer()
        {
            // get the assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            // find every IBeerXMLEntity type in the assembly that is a non-abstract class
            foreach (Type type in assembly.GetTypes().Where(t => typeof(IBeerXmlEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract))
            {
                IList<BeerXMLProperty> propInfoList = new List<BeerXMLProperty>();

                // find every property that has the BeerXMLIncludeAttribute on it
                foreach (PropertyInfo property in type.GetProperties())
                {
                    BeerXMLIncludeAttribute attribute = property.GetCustomAttribute(typeof(BeerXMLIncludeAttribute), inherit: true) as BeerXMLIncludeAttribute;

                    if (attribute != null)
                    {
                        propInfoList.Add(new BeerXMLProperty(property, attribute, typeof(IBeerXmlEntity).IsAssignableFrom(property.PropertyType)));
                    }
                }

                // add to dictionary
                _typeToPropertyMap.Add(type, propInfoList);
            }
        }

        /// <summary>
        /// Serializes the given IBeerXmlEntity object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Serialize(IBeerXmlEntity obj)
        {
            XDocument document = GetBeerXDocument(obj);
            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriter(builder))
            {
                document.Save(writer);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates an XDocument from the given IBeerXmlEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private XDocument GetBeerXDocument(IBeerXmlEntity obj)
        {
            XDocument document = new XDocument(new XDeclaration(Constants.XML_VERSION, Constants.XML_ENCODING, Constants.XML_STANDALONE_YES));

            document.Add(GetBeerXElement(obj));

            return document;
        }

        /// <summary>
        /// Creates an XElement from the given IBeerXmlEntity
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private XElement GetBeerXElement(IBeerXmlEntity obj)
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
            foreach (IBeerXmlEntity record in obj)
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

            foreach (BeerXMLProperty beerXmlProperty in _typeToPropertyMap[objType])
            {
                string propertyName = beerXmlProperty.Name;

                object propertyValue = beerXmlProperty.Property.GetValue(obj);

                if (ShouldAddProperty(beerXmlProperty.Attribute, propertyValue))
                {
                    if (beerXmlProperty.IsIBeerXMLEntity)
                    {
                        element.Add(GetBeerXElement((IBeerXmlEntity)propertyValue));
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
    }
}
