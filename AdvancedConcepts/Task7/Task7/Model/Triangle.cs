namespace Task7.Model {
    public class Triangle : Shape {
        /// <summary>
        /// Gets the base length of the triangle.
        /// </summary>
        public double BaseLength { get; }

        /// <summary>
        /// Gets the height of the triangle.
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class with the specified dimensions.
        /// </summary>
        /// <param name="baseLength">The base length of the triangle.</param>
        /// <param name="height">The height of the triangle.</param>
        public Triangle(double baseLength, double height) : base("Triangle") {
            BaseLength = baseLength;
            Height = height;
        }

        /// <summary>
        /// Calculates the area of the Triangle.
        /// </summary>
        /// <returns>Area of the Triangle</returns>
        public override double CalculateArea() => 0.5 * BaseLength * Height;
    }
}
