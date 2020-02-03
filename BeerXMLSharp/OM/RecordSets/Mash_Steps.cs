using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of MashStep
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Mash_Step}" />
    public sealed class Mash_Steps : BeerXMLRecordSetBase<Mash_Step>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mash_Steps"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Mash_Steps(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mash_Steps"/> class.
        /// </summary>
        public Mash_Steps() : base()
        {
        }
    }
}
