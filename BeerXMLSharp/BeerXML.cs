using BeerXMLSharp.OM;
using BeerXMLSharp.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeerXMLSharp
{
    public static class BeerXML
    {
        private static IBeerXMLSerializer _serializer = null;

        /// <summary>
        /// Gets the serializer used to perform the serialization operations.
        /// </summary>
        /// <value>
        /// The serializer.
        /// </value>
        internal static IBeerXMLSerializer Serializer
        {
            get
            {
                if (_serializer == null)
                {
                    _serializer = new XDocumentBeerXMLSerializer();
                }

                return _serializer;
            }
            set
            {
                _serializer = value;
            }
        }

        /// <summary>
        /// Indicates whether StrictMode is enabled. If true,
        /// an exception will be thrown when deserializing unknown
        /// tags. If false (default), unknown tags are ignored
        /// </summary>
        public static bool StrictModeEnabled
        {
            get
            {
                return Serializer.StrictModeEnabled;
            }
            set
            {
                Serializer.StrictModeEnabled = value;
            }
        }

        /// <summary>
        /// Entry point for deserialization from a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IBeerXMLEntity Deserialize(string filePath)
        {
            return Serializer.Deserialize(filePath);
        }

        /// <summary>
        /// Entry point for deserialization from a stream
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static IBeerXMLEntity Deserialize(Stream stream)
        {
            return Serializer.Deserialize(stream);
        }
    }
}
