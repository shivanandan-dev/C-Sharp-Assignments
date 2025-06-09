namespace Task7.Model {
    public class Circle : Shape {
        /// <summary>
        /// Gets the radius of the circle.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        public Circle(double radius) : base("Circle") {
            Radius = radius;
        }

        /// <summary>
        /// Calculates the area of the Circle.
        /// </summary>
        /// <returns>Area of the Circle</returns>
        public override double CalculateArea() => Math.PI * Radius * Radius;
    }

}
