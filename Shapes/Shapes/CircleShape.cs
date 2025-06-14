﻿namespace ShapesApplication {
    public class CircleShape : Shape {
        /// <summary>
        /// Gets the radius of the object. This property is read-only and represents
        /// the distance from the center of the object to its edge in a circular or spherical context.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// Overrides the base Name to “Circle”.
        /// </summary>
        public override string Name => "Circle";

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
        /// Handles the process of creating a circle by taking user input and displaying details.
        /// </summary>
        public static void CreateCircle() {
            string color = InputManager.GetColorValue();
            if (color is null) return;

            double radius = InputManager.GetLinearMeasurement(nameof(radius));
            if (radius < 0) return;

            CircleShape circle = new CircleShape(color, radius);
            Console.Write("[Info] ");
            circle.PrintDetails();
        }
    }
}
