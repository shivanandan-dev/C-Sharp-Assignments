namespace ShapesApplication {
    public class CircleShape : Shape {
        /// <summary>
        /// Gets the radius of the object. This property is read-only and represents
        /// the distance from the center of the object to its edge in a circular or spherical context.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// Initializes a new instance of the CircleShape class with a specified color and radius.
        /// </summary>
        /// <param name="color">The color of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        public CircleShape(string color, double radius) : base(color) {
            Radius = radius;
        }

        /// <summary>
        /// Calculates and returns the area of the circle.
        /// </summary>
        /// <returns>The area of the circle.</returns>
        public override double CalculateArea() => Math.PI * Math.Pow(Radius, 2);

        /// <summary>
        /// Prints the shape type, color, and area of the circle.
        /// </summary>
        public override void PrintDetails() {
            Console.Write("Shape: Circle, ");
            base.PrintDetails();
        }
    }
}
