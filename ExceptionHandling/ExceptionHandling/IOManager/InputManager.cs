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
                if (MenuInputValidator(choice, isNumber, menuOptions)) {
                    return choice;
                }
            } while (true);
        }

        /// <summary>
        /// Reads and parses a decimal input from the user.
        /// </summary>
        /// <returns>The parsed decimal value.</returns>
        /// <exception cref="InvalidUserInputException">Thrown when the input is not a valid decimal.</exception>
        public static decimal GetDecimalValue() {
            string input = Console.ReadLine();
            bool isDecimal = decimal.TryParse(input, out decimal number);

            if (!isDecimal)
                throw new InvalidUserInputException("[Error] Invalid input. Please enter a valid decimal.");

            return number;
        }

        /// <summary>
        /// Reads and parses an integer input from the user.
        /// </summary>
        /// <returns>The parsed integer value.</returns>
        /// <exception cref="InvalidUserInputException">Thrown when the input is not a valid integer.</exception>
        public static int GetInteger() {
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out int number);

            if (!isNumber) {
                throw new InvalidUserInputException("[Error] Invalid input. Please enter a valid integer.");
            }

            return number;
        }

        /// <summary>
        /// Prompts the user to press any key to continue execution.
        /// </summary>
        public static void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Validates the user's menu input based on whether it is a valid number 
        /// and whether it falls within the range of available menu options.
        /// </summary>
        /// <param name="choice">The user's menu choice input (as an integer).</param>
        /// <param name="isNumber">Indicates whether the input is a valid number.</param>
        /// <param name="menuOptions">The list of menu options to validate against.</param>
        /// <returns>
        /// Returns true if the input is a valid number and within the range of menu options; 
        /// otherwise, returns false and displays an appropriate error message.
        /// </returns>
        private static bool MenuInputValidator(int choice, bool isNumber, List<string> menuOptions) {
            int menuSize = menuOptions.Count;
            if (isNumber && choice >= 1 && choice <= menuSize) {
                return true;
            } else if (isNumber) {
                Console.WriteLine($"[Error] Invalid input. Menu choice must be in the range (1 - {menuSize})");
            } else {
                Console.WriteLine("[Error] Invalid input. Menu choice must be number.");
            }
            return false;
        }
    }
}