using BeerXMLSharp.OM;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.Utilities
{
    /// <summary>
    /// Exception thrown when attempting to serialize an invalid IBeerXMLEntity
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BeerXMLInvalidObjectException : Exception
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public ValidationCode ErrorCode { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLInvalidObjectException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public BeerXMLInvalidObjectException(ValidationCode errorCode) : base()
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLInvalidObjectException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        public BeerXMLInvalidObjectException(ValidationCode errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }

    /// <summary>
    /// Exception thrown when deserialization encounters an XML element
    /// with no corresponding type
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class BeerXMLUnknownTypeTagException : Exception
    {
        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLUnknownTypeTagException"/> class.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        public BeerXMLUnknownTypeTagException(string tagName) : base()
        {
            this.TagName = tagName;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="BeerXMLUnknownTypeTagException"/> class.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <param name="message">The message.</param>
        public BeerXMLUnknownTypeTagException(string tagName, string message) : base(message)
        {
            this.TagName = tagName;
        }
    }
}
