using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Style
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Style}" />
    public sealed class Styles : BeerXMLRecordSetBase<Style>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Styles"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Styles(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Styles"/> class.
        /// </summary>
        public Styles() 
        {

        }
    }
}
