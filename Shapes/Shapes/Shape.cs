namespace ShapeApplication {
    public abstract class Shape {
        public string Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the Shape class with a color.
        /// </summary>
        /// <param name="color">The color of the shape.</param>
        public Shape(string color) {
            Color = color;
        }

        /// <summary>
        /// Abstract method to calculate and return the area of the shape.
        /// </summary>
        /// <returns>The area of the shape.</returns>
        public abstract double CalculateArea();

        /// <summary>
        /// Prints the color and area of the shape.
        /// </summary>
        public virtual void PrintDetails() {
            Console.WriteLine($"Color: {Color}, Area: {CalculateArea()}");
        }
    }
}
