namespace ShapesApplication {
    internal class InputManager {
        /// <summary>
        /// Prompts the user to enter a color for the Circle and validates the input.
        /// </summary>
        /// <returns>
        /// Returns a valid color string if the input is valid; otherwise, returns default (null).
        /// </returns>
        public static string GetColorValue() {
            List<string> validColors = new List<string>() { "Red", "Orange", "Yellow", "Green", "Blue", "Purple",
            "Red-Orange", "Yellow-Orange", "Yellow-Green", "Blue-Green", "Blue-Purple", "Red-Purple" };

            Console.WriteLine($"\nValid Colors: [{String.Join(",", validColors)}]");

            Console.Write("Enter the color of the shape: ");
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
        public static double GetLinearMeasurement(string dimension) {
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

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        public static void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
