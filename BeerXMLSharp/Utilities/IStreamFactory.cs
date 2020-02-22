using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeerXMLSharp.Utilities
{
    internal interface IStreamFactory
    {
        Stream GetFileStream(string filePath, FileMode mode);
    }
}
