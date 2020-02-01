using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM
{
    /// <summary>
    /// Represents a BeerXML Record such as HOP, FERMENTABLE, etc.
    /// </summary>
    public interface IRecord : IBeerXmlEntity
    {
        /// <summary>
        /// Every record has a name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Every record has a version
        /// </summary>
        int Version { get; set; }
    }
}
