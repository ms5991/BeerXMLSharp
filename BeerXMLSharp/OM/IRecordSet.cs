using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM
{
    /// <summary>
    /// Represents a set of records such as HOPS, FERMENTABLES, etc. Allows enumeration over
    /// the records in the set
    /// </summary>
    public interface IRecordSet : IBeerXMLEntity, IEnumerable<IRecord>
    {
        /// <summary>
        /// Add a record to this set
        /// </summary>
        /// <param name="child">Child record to add</param>
        void Add(IRecord child);

        /// <summary>
        /// Removes a record from this set
        /// </summary>
        /// <param name="child">Child record to remove</param>
        void Remove(IRecord child);

        /// <summary>
        /// Number of items in this set
        /// </summary>
        int Count { get; }
    }
}
