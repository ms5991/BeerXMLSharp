using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeerXMLSharp.Utilities
{
    internal class FileStreamFactory : IStreamFactory
    {
        public Stream GetFileStream(string filePath, FileMode mode)
        {
            return new FileStream(filePath, mode);
        }
    }
}
