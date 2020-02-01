using System;
using System.Collections.Generic;
using System.Text;

namespace BeerXMLSharp.Utilities
{
    internal static class DoubleExtensions
    {
        /// <summary>
        /// Determines if the given double values are equal within an epsilon
        /// </summary>
        /// <param name="firstValue">The first value.</param>
        /// <param name="secondValue">The second value.</param>
        /// <param name="epsilon">The epsilon.</param>
        /// <returns>
        ///   <c>true</c> if [is equal with epsilon] [the specified second value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEqualWithEpsilon(this double firstValue, double secondValue, double epsilon = 0.001)
        {
            return Math.Abs(firstValue - secondValue) <= epsilon;
        }
    }
}
