﻿using BeerXMLSharp.Serialization;
using BeerXMLSharp.Utilities;
using System;
using System.Collections.Generic;
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

        internal bool RequiredPropertiesNonNull(ref ValidationCode errorCode)
        {
            IDictionary<string, BeerXMLProperty> typeProperties = BeerXMLProperty.GetBeerXMLPropertyList(this.GetType());

            foreach (KeyValuePair<string, BeerXMLProperty> pair in typeProperties)
            {
                // if the property is a required property and it's type 
                // can be null, make sure it's not null
                if (pair.Value.Attribute.Requirement == PropertyRequirement.REQUIRED &&
                    !pair.Value.Property.PropertyType.IsValueType)
                {
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
        /// Returns a string containing the BeerXML representing this instance
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BeerXMLInvalidObjectException"></exception>
        public string GetBeerXML()
        {
            ValidationCode code = ValidationCode.SUCCESS;
            if (this.IsValid(ref code))
            {
                return this.Serializer.Serialize(this);
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
