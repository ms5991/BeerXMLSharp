using BeerXMLSharp.OM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeerXMLSharp.Serialization
{
    /// <summary>
    /// Type used to serialize IBeerXMLEntities to BeerXML
    /// </summary>
    internal interface IBeerXMLSerializer
    {

        /// <summary>
        /// Serializes the specified IBeerXMLEntity to BeerXML and output
        /// to the given file.
        /// </summary>
        /// <param name="obj">The IBeerXMLEntity object.</param>
        /// <returns></returns>
        void Serialize(IBeerXMLEntity obj, string filePath);

        /// <summary>
        /// Serializes the specified IBeerXMLEntity to BeerXML and output
        /// to the given stream.
        /// </summary>
        /// <param name="obj">The IBeerXMLEntity object.</param>
        /// <returns></returns>
        void Serialize(IBeerXMLEntity obj, Stream stream);

        /// <summary>
        /// Deserializes the contents of the specified file path into
        /// and IBeerXMLEntity object
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IBeerXMLEntity Deserialize(string filePath);

        /// <summary>
        /// Deserializes the contents of the specified file path into
        /// and IBeerXMLEntity object
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        IBeerXMLEntity Deserialize(Stream stream);
    }
}
