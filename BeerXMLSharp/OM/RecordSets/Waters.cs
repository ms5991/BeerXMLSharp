using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Water
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Water}" />
    public class Waters : BeerXMLRecordSetBase<Water>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Waters"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Waters(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Waters"/> class.
        /// </summary>
        public Waters()
        {
        }
    }
}
