using Task7.Model;

namespace Task7 {
    internal class Program {
        /// <summary>
        /// Application entry point. Creates shapes and displays their details.
        /// </summary>
        public static void Main() {
            var shapes = new List<Shape>
            {
                new Circle(5),
                new Rectangle(4, 6),
                new Triangle(3, 8),
                null
            };

            foreach (var shape in shapes) {
                DisplayShapeDetails(shape);
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Prints the details and area of the given shape using type patterns.
        /// </summary>
        /// <param name="shape">The <see cref="Shape"/> instance whose details and area will be displayed. May be null.</param>
        static void DisplayShapeDetails(Shape shape) {
            switch (shape) {
                case null:
                    Console.WriteLine("Shape is null.");
                    break;
                case Circle c:
                    Console.WriteLine($"Shape: {c.Name}, Radius: {c.Radius}");
                    Console.WriteLine($"Area: {c.CalculateArea():F2}");
                    break;
                case Rectangle r:
                    Console.WriteLine($"Shape: {r.Name}, Width: {r.Width}, Height: {r.Height}");
                    Console.WriteLine($"Area: {r.CalculateArea():F2}");
                    break;
                case Triangle t:
                    Console.WriteLine($"Shape: {t.Name}, Base: {t.BaseLength}, Height: {t.Height}");
                    Console.WriteLine($"Area: {t.CalculateArea():F2}");
                    break;
                default:
                    Console.WriteLine("Unknown shape type.");
                    break;
            }
        }
    }
}
