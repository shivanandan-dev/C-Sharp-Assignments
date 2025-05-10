namespace ShapesApplication {
    public class ShapeApplication {
        /// <summary>
        /// Displays the menu options to the user.
        /// </summary>
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"\n{menuTitle}");
            foreach (var menuAction in menuActions) {
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            }
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        public static void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Handles the process of creating a rectangle by taking user input and displaying details.
        /// </summary>
        public static void CreateRectangle() {
            Console.Write("Enter the color of the rectangle: ");
            string color = Console.ReadLine();

            double length = GetValidatedDoubleValue("Enter length: ");
            if (length < 0) return;

            double width = GetValidatedDoubleValue("Enter width: ");
            if (width < 0) return;

            RectangleShape rectangle = new RectangleShape(color, length, width);
            Console.Write("[Info] ");
            rectangle.PrintDetails();
        }

        /// <summary>
        /// Handles the process of creating a circle by taking user input and displaying details.
        /// </summary>
        public static void CreateCircle() {
            Console.Write("Enter the color of the rectangle: ");
            string color = Console.ReadLine();

            double radius = GetValidatedDoubleValue("Enter length: ");
            if (radius < 0) return;

            CircleShape circle = new CircleShape(color, radius);
            Console.Write("[Info] ");
            circle.PrintDetails();
        }

        /// <summary>
        /// Prompts the user for a double input and validates it.
        /// </summary>
        /// <param name="prompt">The prompt message to display.</param>
        /// <returns>A valid double value or -1 if input is invalid.</returns>
        static double GetValidatedDoubleValue(string prompt) {
            Console.Write(prompt);
            string input = Console.ReadLine();
            bool isValid = double.TryParse(input, out double value);

            if (!isValid) {
                Console.WriteLine("[Error] Invalid input. Try again!");
                return -1;
            }

            return value;
        }
    }
}
