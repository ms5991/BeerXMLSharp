using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.Utilities
{
    /// <summary>
    /// Helper static class to perform basic validation on parameters
    /// </summary>
    internal static class Validation
    {
        /// <summary>
        /// Validates that the given value is between 0 and 100
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidatePercentileRange(double percent)
        {
            if (percent > 100 || percent < 0)
            {
                throw new ArgumentException(string.Format("Value [{0}] is not within the range [0,100]", percent));
            }
        }

        /// <summary>
        /// Validates whether the given double is greater than zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateGreaterThanZero(double value)
        {
            if (value <= 0)
            {
                throw new ArgumentException(string.Format("Value [{0}] must be greater than zero!", value));
            }
        }

        /// <summary>
        /// Validates whether the given int is greater than zero.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateGreaterThanZero(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException(string.Format("Value [{0}] must be greater than zero!", value));
            }
        }

        /// <summary>
        /// Validates whether the given object is not null.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <exception cref="ArgumentException">Value of parameter cannot be null!</exception>
        public static void ValidateNotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("Value of parameter cannot be null!");
            }
        }
    }
}
