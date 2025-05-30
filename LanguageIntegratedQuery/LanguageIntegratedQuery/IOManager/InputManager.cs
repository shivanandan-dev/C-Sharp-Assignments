using LanguageIntegratedQuery.Model;

namespace LanguageIntegratedQuery.IOManager {
    internal class InputManager {
        /// <summary>
        /// Prompts the user to enter a choice for the menu and validates the input.
        /// Returns the zero-based index of the selected menu option.
        /// </summary>
        /// <param name="menuOptions">List of menu options to display.</param>
        /// <returns>Zero-based index of the selected menu option.</returns>
        public static int GetMenuChoice(List<Menu> menuOptions) {
            Console.WriteLine();
            do {
                Console.Write("[Menu] Enter Choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);
                if (MenuInputValidator(choice, isNumber, menuOptions)) {
                    return choice - 1;
                }
            } while (true);
        }

        /// <summary>
        /// Prompts the user to press any key to continue, pausing the application until input is received.
        /// </summary>
        public static void PromptForContinuation() {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        /// <summary>
        /// Validates the user's menu input to ensure it is a number and within the menu options range.
        /// </summary>
        /// <param name="choice">The user's menu choice as an integer.</param>
        /// <param name="isNumber">Indicates whether the user's input is a valid number.</param>
        /// <param name="menuOptions">List of menu options to check against.</param>
        /// <returns>True if input is valid; otherwise, false.</returns>
        private static bool MenuInputValidator(int choice, bool isNumber, List<Menu> menuOptions) {
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

