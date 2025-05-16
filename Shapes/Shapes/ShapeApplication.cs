namespace ShapesApplication {
    public class ShapeApplication {
        /// <summary>
        /// Displays a menu of options to the user.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">
        /// A dictionary where the key represents the menu option number, 
        /// and the value is a tuple containing the description of the option 
        /// and the associated action to perform.
        /// </param>
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
            string color = GetColorValue();
            if (color == default(string)) return;

            double length = GetLinearMeasurement(nameof(length));
            if (length < 0) return;

            double width = GetLinearMeasurement(nameof(width));
            if (width < 0) return;

            RectangleShape rectangle = new RectangleShape(color, length, width);
            Console.Write("[Info] ");
            rectangle.PrintDetails();
        }

        /// <summary>
        /// Handles the process of creating a circle by taking user input and displaying details.
        /// </summary>
        public static void CreateCircle() {
            string color = GetColorValue();
            if (color == default(string)) return;

            double radius = GetLinearMeasurement(nameof(radius));
            if (radius < 0) return;

            CircleShape circle = new CircleShape(color, radius);
            Console.Write("[Info] ");
            circle.PrintDetails();
        }

        /// <summary>
        /// Prompts the user to enter a color for the Circle and validates the input.
        /// </summary>
        /// <returns>
        /// Returns a valid color string if the input is valid; otherwise, returns default (null).
        /// </returns>
        public static string GetColorValue() {
            List<string> validColors = new List<string>() { "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
            "Red-Orange", "Yellow-Orange", "Yellow-Green", "Blue-Green", "Blue-Purple", "Red-Purple" };

            Console.Write("\nValid Colors: [");

            for (int index = 0; index < validColors.Count; index++) {
                string seperator = index == validColors.Count - 1 ? null : ", ";
                Console.Write($"{validColors[index]}{seperator}");
            }
            Console.WriteLine("]");

            Console.Write("Enter the color of the Circle: ");
            string color = Console.ReadLine();
            if (validColors.Contains(color))
                return color;
            else {
                Console.WriteLine("[Error] Invalid input. Must be a valid color");
                return default;
            }

        }

        /// <summary>
        /// Prompts the user to enter a double value for a specified dimension,
        /// validates the input, and returns the value if valid.
        /// </summary>
        /// <param name="dimension">The name or description of the dimension to prompt for (e.g., "length", "width").</param>
        /// <returns>
        /// Returns the entered double value if it is valid and non-negative; 
        /// returns -1 if the input is not a valid double or is negative.
        /// </returns>
        private static double GetLinearMeasurement(string dimension) {
            Console.Write($"Enter {dimension}: ");
            string input = Console.ReadLine();
            bool isValid = double.TryParse(input, out double value);

            if (!isValid) {
                Console.WriteLine("[Error] Invalid input. Try again!");
                return -1;
            } else if (value < 0) {
                Console.WriteLine($"[Error] Invalid input. Must be a positive double value.");
                return -1;
            }

            return value;
        }
    }
}
