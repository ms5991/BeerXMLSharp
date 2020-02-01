using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM
{
    internal enum PropertyRequirement
    {
        OPTIONAL,
        REQUIRED,
        CONDITIONAL
    }
    
    /// <summary>
    /// Attribute used to indicate whether a property should be included in BeerXML
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    internal sealed class BeerXMLIncludeAttribute : Attribute
    {
        /// <summary>
        /// Indicates whether the property is required, optional, or conditional.
        /// </summary>
        /// <value>
        /// The requirement.
        /// </value>
        public PropertyRequirement Requirement { get; }

        /// <summary>
        /// Indicates whether this property is an extension property
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is extension; otherwise, <c>false</c>.
        /// </value>
        public bool IsExtension { get; }

        /// <summary>
        /// Indicates the minimum supported version of BeerXML for this property. Default is Constants.DEFAULT_BEER_XML_VERSION
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public int Version { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLIncludeAttribute"/> class.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        /// <param name="isExtension">if set to <c>true</c> [is extension].</param>
        /// <param name="version">The version.</param>
        public BeerXMLIncludeAttribute(PropertyRequirement requirement = PropertyRequirement.OPTIONAL, bool isExtension = false, int version = Constants.DEFAULT_BEER_XML_VERSION)
        {
            this.Requirement = requirement;
            this.IsExtension = isExtension;
            this.Version = version;
        }
    }
}
