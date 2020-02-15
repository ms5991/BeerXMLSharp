using BeerXMLSharp.OM.Records;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM.RecordSets
{
    /// <summary>
    /// Record set of Recipe
    /// </summary>
    /// <seealso cref="BeerXMLSharp.OM.RecordSets.BeerXMLRecordSetBase{BeerXMLSharp.OM.Records.Recipe}" />
    public class Recipes : BeerXMLRecordSetBase<Recipe>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipes"/> class.
        /// </summary>
        /// <param name="children">The children.</param>
        public Recipes(IList<IRecord> children) : base(children)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Recipes"/> class.
        /// </summary>
        public Recipes()
        {
        }
    }
}
