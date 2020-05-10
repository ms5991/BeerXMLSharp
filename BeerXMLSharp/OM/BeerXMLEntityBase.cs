using BeerXMLSharp.Serialization;
using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM
{
    /// <summary>
    /// Base class for all BeerXML entities
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.IBeerXMLEntity" />
    public abstract class BeerXMLEntityBase : IBeerXMLEntity
    {
        /// <summary>
        /// Serializer used to serialize this instance to BeerXML
        /// </summary>
        internal IBeerXMLSerializer Serializer
        {
            get
            {
                return BeerXML.Serializer;
            }
            set
            {
                BeerXML.Serializer = value;
            }
        }

        /// <summary>
        /// Indicates whether serialization methods should throw
        /// an exception when IsValid would return false
        /// </summary>
        public bool AllowInvalidSerialization
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        public bool IsValid()
        {
            ValidationCode errorCode = ValidationCode.SUCCESS;
            return this.IsValid(ref errorCode);
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        public virtual bool IsValid(ref ValidationCode errorCode)
        {
            return RequiredPropertiesNonNull(ref errorCode);
        }

        /// <summary>
        /// Ensures that missing required properties result in a non-valid entity
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        internal bool RequiredPropertiesNonNull(ref ValidationCode errorCode)
        {
            IDictionary<string, BeerXMLProperty> typeProperties = BeerXMLProperty.GetBeerXMLPropertyList(this.GetType());

            foreach (KeyValuePair<string, BeerXMLProperty> pair in typeProperties)
            {
                // if the property is a required property and it's type 
                // can be null, make sure it's not null.  Since value types could
                // conceivably validly be their default value, it's better to assume
                // that they are always valid and let the semantic validation be
                // performed by the program using this library
                if (pair.Value.Attribute.Requirement == PropertyRequirement.REQUIRED &&
                    !pair.Value.Property.PropertyType.IsValueType)
                {
                    // the value of the property should be non-null.
                    if (pair.Value.Property.GetValue(this) == null)
                    {
                        errorCode |= ValidationCode.MISSING_REQUIRED_PROPERTY;
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Outputs the BeerXML representing this instance to the given file
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BeerXMLInvalidObjectException"></exception>
        public ValidationCode GetBeerXML(string filePath)
        {
            ValidationCode code = ValidationCode.SUCCESS;
            if (this.IsValid(ref code) || this.AllowInvalidSerialization)
            {
                this.Serializer.Serialize(this, filePath);
                return code;
            }

            throw new BeerXMLInvalidObjectException(code, string.Format("Validation failed for [{0}] with code [{1}]", this.ToString(), code.ToString()));
        }

        /// <summary>
        /// Outputs the BeerXML representing this instance to the given stream
        /// </summary>
        /// <returns></returns
        /// <exception cref="BeerXMLInvalidObjectException"></exception>
        public ValidationCode GetBeerXML(Stream stream)
        {
            ValidationCode code = ValidationCode.SUCCESS;
            if (this.IsValid(ref code) || this.AllowInvalidSerialization)
            {
                this.Serializer.Serialize(this, stream);

                return code;
            }

            throw new BeerXMLInvalidObjectException(code, string.Format("Validation failed for [{0}] with code [{1}]", this.ToString(), code.ToString()));
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.GetType().Name.ToUpperInvariant();
        }
    }
}
