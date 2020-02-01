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
        public BeerXMLInvalidObjectException(string message, ValidationCode errorCode) : base (message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
