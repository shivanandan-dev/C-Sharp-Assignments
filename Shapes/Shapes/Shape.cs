namespace ShapesApplication {
    public abstract class Shape {
        /// <summary>
        /// Represents the color of the object.
        /// </summary>
        public string Color { get; set; }

        public virtual string Name { get; protected set; }

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
            Console.WriteLine($"Shape: {Name}, Color: {Color}, Area: {CalculateArea()}");
        }
    }
}
