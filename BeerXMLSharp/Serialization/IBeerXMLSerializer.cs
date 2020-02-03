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
        /// Serializes the specified IBeerXMLEntity to BeerXML.
        /// </summary>
        /// <param name="obj">The IBeerXMLEntity object.</param>
        /// <returns></returns>
        string Serialize(IBeerXMLEntity obj);

        /// <summary>
        /// Serializes the specified IBeerXMLEntity to BeerXML and output
        /// to the given file.
        /// </summary>
        /// <param name="obj">The IBeerXMLEntity object.</param>
        /// <returns></returns>
        void Serialize(IBeerXMLEntity obj, string filePath);

        /// <summary>
        /// Deserializes the contents of the specified file path into
        /// and IBeerXMLEntity object
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IBeerXMLEntity Deserialize(string filePath);
    }
}
