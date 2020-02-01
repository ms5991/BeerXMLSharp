using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Mash
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Mash}" />
    public sealed class Mashs : BeerXMLRecordSetBase<Mash>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mashs"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Mashs(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mashs"/> class.
        /// </summary>
        public Mashs()
        {
        }
    }
}
