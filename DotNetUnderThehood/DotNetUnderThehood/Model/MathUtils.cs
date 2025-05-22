using System.Numerics;

namespace DotNetUnderThehood.Model {
    public static class MathUtils {
        /// <summary>
        /// Returns the sum of two values.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="INumber{T}"/>.</typeparam>
        /// <param name="a">The first value to add.</param>
        /// <param name="b">The second value to add.</param>
        /// <returns>The sum of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static T Add<T>(T a, T b) where T : INumber<T> => a + b;

        /// <summary>
        /// Returns the result of subtracting one value from another.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="INumber{T}"/>.</typeparam>
        /// <param name="a">The value to subtract from.</param>
        /// <param name="b">The value to subtract.</param>
        /// <returns>The result of <paramref name="a"/> minus <paramref name="b"/>.</returns>
        public static T Sub<T>(T a, T b) where T : INumber<T> => a - b;

        /// <summary>
        /// Returns the product of two values.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="INumber{T}"/>.</typeparam>
        /// <param name="a">The first value to multiply.</param>
        /// <param name="b">The second value to multiply.</param>
        /// <returns>The product of <paramref name="a"/> and <paramref name="b"/>.</returns>
        public static T Mul<T>(T a, T b) where T : INumber<T> => a * b;

        /// <summary>
        /// Returns the result of dividing one value by another.
        /// </summary>
        /// <typeparam name="T">A type that implements <see cref="INumber{T}"/>.</typeparam>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <returns>The result of <paramref name="a"/> divided by <paramref name="b"/>.</returns>
        /// <exception cref="Exception">Thrown when division by zero occurs.</exception>
        public static T Div<T>(T a, T b) where T : INumber<T> {
            try {
                return a / b;
            } catch (DivideByZeroException e) {
                throw new Exception(e.Message);
            }
        }
    }
}
