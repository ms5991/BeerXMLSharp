using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Misc
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Misc}" />
    public sealed class Miscs : BeerXMLRecordSetBase<Misc>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Miscs"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Miscs(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Miscs"/> class.
        /// </summary>
        public Miscs()
        {
        }
    }
}
