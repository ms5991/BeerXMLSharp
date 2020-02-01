using BeerXMLSharp.OM;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.Serialization
{
    /// <summary>
    /// Type used to serialize IBeerXMLEntities to BeerXML
    /// </summary>
    internal interface IBeerXMLSerializer
    {
        /// <summary>
        /// Serializes the specified IBeerXmlEntity to BeerXML.
        /// </summary>
        /// <param name="obj">The IBeerXmlEntity object.</param>
        /// <returns></returns>
        string Serialize(IBeerXmlEntity obj);
    }
}
