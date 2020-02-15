using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Yeast
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Yeast}" />
    public class Yeasts : BeerXMLRecordSetBase<Yeast>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Yeasts"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Yeasts(IList<IRecord> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Yeasts"/> class.
        /// </summary>
        public Yeasts()
        {
        }
    }
}
