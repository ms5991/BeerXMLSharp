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

        /// <summary>
        /// Determines whether this type is int32.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is int32; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInt32(this Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.Int32;
        }

    }
}
