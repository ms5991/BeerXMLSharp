﻿using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Fermentable
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Fermentable}" />
    public class Fermentables : BeerXMLRecordSetBase<Fermentable>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Fermentables"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Fermentables(IList<IRecord> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fermentables"/> class.
        /// </summary>
        public Fermentables()
        {
        }
    }
}
