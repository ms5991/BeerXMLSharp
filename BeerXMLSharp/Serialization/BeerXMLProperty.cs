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
        private static readonly IDictionary<Type, IDictionary<string, BeerXMLProperty>> _typeToPropertyMap = new Dictionary<Type, IDictionary<string, BeerXMLProperty>>();

        /// <summary>
        /// Maps the name of a type to the Type
        /// </summary>
        private static readonly IDictionary<string, Type> _typeNameToTypeMap = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Indicates if the dictionaries have been initialized
        /// </summary>
        private static bool _isInit = false;

        /// <summary>
        /// Ensures the property mapping dictionaries are built.
        /// </summary>
        private static void EnsurePropertyMappings()
        {
            // dictionaries are built, just return
            if (_isInit)
            {
                return;
            }

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

            // don't run this again
            _isInit = true;
        }

        /// <summary>
        /// Gets the beer XML property list for the given type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IDictionary<string, BeerXMLProperty> GetBeerXMLPropertyList(Type type)
        {
            EnsurePropertyMappings();
            return _typeToPropertyMap[type];
        }

        /// <summary>
        /// Gets the Type corresponding to the given name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Type TryGetTypeByName(string name)
        {
            EnsurePropertyMappings();

            if (_typeNameToTypeMap.TryGetValue(name, out Type type))
            {
                return type;
            }

            return null;
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
