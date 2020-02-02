using BeerXMLSharp.OM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BeerXMLSharp.Serialization
{
    /// <summary>
    /// Private wrapper class to represent a property that should
    /// be included in the beer XML serialization
    /// </summary>
    internal class BeerXMLProperty
    {
        /// <summary>
        /// Static dictionary used to map types to property info, to avoid some extra reflection calls
        /// at serialization time (do it all up front)
        /// </summary>
        private static readonly IDictionary<Type, IList<BeerXMLProperty>> _typeToPropertyMap = new Dictionary<Type, IList<BeerXMLProperty>>();

        private static bool _isInit = false;

        private static void BuildTypeToPropertyMap()
        {
            // get the assembly
            Assembly assembly = Assembly.GetExecutingAssembly();

            var types = assembly.GetTypes().Where(t => typeof(IBeerXMLEntity).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();
            // find every IBeerXMLEntity type in the assembly that is a non-abstract class
            foreach (Type type in types)
            {
                IList<BeerXMLProperty> propInfoList = new List<BeerXMLProperty>();

                // find every property that has the BeerXMLIncludeAttribute on it
                foreach (PropertyInfo property in type.GetProperties())
                {
                    BeerXMLIncludeAttribute attribute = property.GetCustomAttribute(typeof(BeerXMLIncludeAttribute), inherit: true) as BeerXMLIncludeAttribute;

                    if (attribute != null)
                    {
                        propInfoList.Add(new BeerXMLProperty(property, attribute, typeof(IBeerXMLEntity).IsAssignableFrom(property.PropertyType)));
                    }
                }

                // add to dictionary
                _typeToPropertyMap.Add(type, propInfoList);
            }
        }

        public static IList<BeerXMLProperty> GetBeerXMLPropertyList(Type type)
        {
            if (!_isInit)
            {
                BuildTypeToPropertyMap();
                _isInit = true;
            }

            return _typeToPropertyMap[type];
        }

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
}
