using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM.Records
{
    public abstract class BeerXMLRecordBase : BeerXMLEntityBase, IRecord
    {
        /// <summary>
        /// Every record has a name
        /// </summary>
        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public string Name { get; set; }

        /// <summary>
        /// Every record has a version
        /// </summary>
        [BeerXMLInclude(requirement: PropertyRequirement.REQUIRED)]
        public int Version { get; set; }

        /// <summary>
        /// Every record has an optional notes property.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        [BeerXMLInclude()]
        public string Notes { get; set; }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        internal virtual bool IsValidInternal(ref ValidationCode errorCode)
        {
            return true;
        }

        /// <summary>
        /// Returns a bool indicating if this instance's conditional properties will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        internal virtual bool ValidateConditionalProperties(ref ValidationCode errorCode)
        {
            return true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLRecordBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="version">The version.</param>
        internal BeerXMLRecordBase(string name, int version)
        {
            this.Name = name;
            this.Version = version;
        }

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        public override sealed bool IsValid(ref ValidationCode errorCode)
        {
            return this.IsValidInternal(ref errorCode) && this.ValidateConditionalProperties(ref errorCode);
        }
    }
}
