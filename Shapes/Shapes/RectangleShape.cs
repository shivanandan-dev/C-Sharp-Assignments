namespace ShapeApplication {
    public class RectangleShape : Shape {
        public double Length { get; set; }
        public double Width { get; set; }

        /// <summary>
        /// Initializes a new instance of the RectangleShape class with color, length, and width.
        /// </summary>
        /// <param name="color">The color of the rectangle.</param>
        /// <param name="length">The length of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        public RectangleShape(string color, double length, double width) : base(color) {
            Length = length;
            Width = width;
        }

        /// <summary>
        /// Calculates and returns the area of the rectangle.
        /// </summary>
        /// <returns>The area of the rectangle.</returns>
        public override double CalculateArea() => Length * Width;

        /// <summary>
        /// Prints the shape type, color, and area of the rectangle.
        /// </summary>
        public override void PrintDetails() {
            Console.Write("Shape: Rectangle, ");
            base.PrintDetails();
        }
    }
}
