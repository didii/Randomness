using System;

namespace Randomness {
    /// <summary>
    ///     Some handy functions to get a random number
    /// </summary>
    static class Rand {
        /// <summary>
        ///     Random instance that is used for every function in <see cref="Rand"/>
        /// </summary>
        private static readonly Random RandInstance = new Random();

        /// <summary>
        ///     Get a random <see cref="int"/>
        /// </summary>
        /// <returns></returns>
        public static int Next() => RandInstance.Next();

        /// <summary>
        ///     Get a random non-negative <see cref="int"/> up to <paramref name="maxValue"/> (non-inclusive)
        /// </summary>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int maxValue) => RandInstance.Next(maxValue);

        /// <summary>
        ///     Get a random <see cref="int"/> between the given values
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static int Next(int minValue, int maxValue) => RandInstance.Next(minValue, maxValue);

        /// <summary>
        ///     Fills the given array with random bytes
        /// </summary>
        /// <param name="buffer"></param>
        public static void NextByte(byte[] buffer) => RandInstance.NextBytes(buffer);

        /// <summary>
        ///     Get a random <see cref="float"/> between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static float NextFloat() => (float)RandInstance.NextDouble();
    }
}
