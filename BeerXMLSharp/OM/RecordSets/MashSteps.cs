using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of MashStep
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.MashStep}" />
    public sealed class MashSteps : BeerXMLRecordSetBase<MashStep>
    {
        /// <summary>
        /// Tag used in BeerXML. Override to include underscore
        /// </summary>
        public override sealed string TagName
        {
            get
            {
                return "Mash_Steps".ToUpperInvariant();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MashSteps"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public MashSteps(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MashSteps"/> class.
        /// </summary>
        public MashSteps()
        {
        }
    }
}
