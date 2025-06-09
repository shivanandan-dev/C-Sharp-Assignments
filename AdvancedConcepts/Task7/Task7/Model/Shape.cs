namespace Task7.Model {
    public abstract class Shape {
        /// <summary>
        /// Gets the name of the shape.
        /// </summary>
        public string Name { get; }

        protected Shape(string name) {
            Name = name;
        }

        /// <summary>
        /// Calculates the area of the shape.
        /// </summary>
        public abstract double CalculateArea();
    }
}
