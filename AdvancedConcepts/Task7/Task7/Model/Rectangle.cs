namespace Task7.Model {
    public class Rectangle : Shape {
        /// <summary>
        /// Gets the width of the rectangle.
        /// </summary>
        public double Width { get; }

        /// <summary>
        /// Gets the height of the rectangle.
        /// </summary>
        public double Height { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class with the specified dimensions.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        public Rectangle(double width, double height) : base("Rectangle") {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Calculates the area of the Rectangle.
        /// </summary>
        /// <returns>Area of the Rectangle</returns>
        public override double CalculateArea() => Width * Height;
    }
}
