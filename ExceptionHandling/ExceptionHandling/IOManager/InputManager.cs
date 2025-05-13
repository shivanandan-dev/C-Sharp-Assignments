using ExceptionHandling.Exceptions;

namespace ExceptionHandling.IOManager {
    public static class InputManager {
        /// <summary>
        /// Gets the user's menu choice from a list of menu options.
        /// </summary>
        /// <param name="menuOptions">The list of menu options available to the user.</param>
        /// <returns>The user's selected menu choice as an integer.</returns>
        public static int GetMenuChoice(List<string> menuOptions) {
            Console.WriteLine();
            do {
                Console.Write("[Menu] Enter Choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);
                int menuSize = menuOptions.Count;
                if (isNumber && choice >= 1 && choice <= menuSize) {
                    return choice;
                } else if (isNumber) {
                    Console.WriteLine($"[Error] Invalid input. Menu choice must be in the range (1 - {menuSize})");
                } else {
                    Console.WriteLine("[Error] Invalid input. Menu choice must be number.");
                }
            } while (true);
        }

        /// <summary>
        /// Reads and parses a decimal input from the user.
        /// </summary>
        /// <returns>The parsed decimal value.</returns>
        /// <exception cref="InvalidUserInputException">Thrown when the input is not a valid decimal.</exception>
        public static decimal GetDecimal() {
            string input = Console.ReadLine();
            bool isDecimal = decimal.TryParse(input, out decimal number);

            if (!isDecimal)
                throw new InvalidUserInputException("[Error] Invalid input. Please enter a valid integer.");

            return number;
        }

        /// <summary>
        /// Reads and parses an integer input from the user.
        /// </summary>
        /// <returns>The parsed integer value.</returns>
        /// <exception cref="InvalidUserInputException">Thrown when the input is not a valid integer.</exception>
        public static int GetNumber() {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out int number);

            if (!isNumber) {
                throw new InvalidUserInputException("[Error] Invalid input. Please enter a valid integer.");
            }

            return number;
        }
    }
}