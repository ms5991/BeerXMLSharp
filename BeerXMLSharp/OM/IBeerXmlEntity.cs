using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BeerXMLSharp.OM
{
    /// <summary>
    /// Beer XML entity
    /// </summary>
    public interface IBeerXMLEntity
    {
        /// <summary>
        /// Returns a string containing the BeerXML representing this instance
        /// </summary>
        /// <returns></returns>
        string GetBeerXML();

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <returns></returns>
        bool IsValid();

        /// <summary>
        /// Returns a bool indicating if this instance will produce valid BeerXML.
        /// </summary>
        /// <param name="errorCode">Reason for validation failure, if applicable</param>
        /// <returns></returns>
        bool IsValid(ref ValidationCode errorCode);
    }
}
