namespace DotNetUnderThehood.Model {
    public static class MathUtils {
        /// <summary>
        /// Returns the sum of two integers.
        /// </summary>
        public static int Add(int a, int b) => a + b;

        /// <summary>
        /// Returns the result of subtracting one integer from another.
        /// </summary>
        public static int Sub(int a, int b) => a - b;

        /// <summary>
        /// Returns the product of two integers.
        /// </summary>
        public static int Mul(int a, int b) => a * b;

        /// <summary>
        /// Returns the result of dividing one integer by another.
        /// </summary>
        /// <exception cref="DivideByZeroException">Thrown when b is zero.</exception>
        public static int Div(int a, int b) {
            if (b == 0)
                throw new DivideByZeroException("Attempted to divide by zero.");
            return a / b;
        }
    }
}
