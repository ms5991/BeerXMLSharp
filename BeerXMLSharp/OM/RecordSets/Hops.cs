using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Hop
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Hop}" />
    public class Hops : BeerXMLRecordSetBase<Hop>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hops"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Hops(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Hops"/> class.
        /// </summary>
        public Hops()
        {
        }
    }
}
