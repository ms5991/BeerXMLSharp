using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Equipment
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Equipment}" />
    public class Equipments : BeerXMLRecordSetBase<Equipment>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Equipments"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Equipments(IList<IBeerXMLEntity> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Equipments"/> class.
        /// </summary>
        public Equipments()
        {
        }
    }
}
