using ExpenseTracker.Model;
using ExpenseTracker.Utils;

namespace ExpenseTracker.IOManager {
    internal class InputManager {
        /// <summary>
        /// Prompts the user to press any key to continue and waits for input.
        /// </summary>
        public static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Gets a valid menu choice from the user based on the provided menu actions.
        /// </summary>
        /// <param name="menuActions">A dictionary of menu options mapping to their descriptions and corresponding actions.</param>
        /// <returns>The user's selected menu choice as an integer.</returns>
        public static int GetMenuChoice(Dictionary<int, (string, Action)> menuActions) {
            Console.WriteLine();
            do {
                Console.Write($"[Menu] Enter choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);
                if (isNumber && menuActions.ContainsKey(choice)) {
                    return choice;
                } else if (isNumber) {
                    OutputManager.DisplayInvalidInputError($"Choice should be between (1 - {menuActions.Count + 1})");
                } else {
                    OutputManager.DisplayInvalidInputError("Invalid Input");
                }
            } while (true);
        }

        /// <summary>
        /// Prompts the user to enter a positive decimal amount and validates the input.
        /// </summary>
        /// <returns>A valid decimal amount entered by the user.</returns>
        public static decimal GetAmount() {
            do {
                Console.Write("Enter Amount: ");
                string input = Console.ReadLine();
                bool isDecimal = decimal.TryParse(input, out decimal amount);

                if (isDecimal && amount > 0) {
                    return amount;
                } else if (isDecimal) {
                    OutputManager.DisplayInvalidInputError("Amount should be a positive decimal");
                } else {
                    OutputManager.DisplayInvalidInputError();
                }
            } while (true);
        }

        /// <summary>
        /// Prompts the user to enter a date in the format dd/mm/yyyy and validates the input.
        /// </summary>
        /// <returns>A valid DateTime object representing the entered date.</returns>
        public static DateTime GetDate() {
            do {
                Console.Write("Enter Date (dd/mm/yyyy): ");
                string input = Console.ReadLine();
                bool isValidDate = Validator.IsValidDateFormat(input);

                if (isValidDate) {
                    return DateTime.Parse(input);
                } else {
                    OutputManager.DisplayInvalidInputError("Invalid Date Format");
                }
            } while (true);
        }

        /// <summary>
        /// Prompts the user to enter an expense ID and validates the input.
        /// </summary>
        /// <param name="expenseDetails">A list of expense details to validate the entered ID against.</param>
        /// <returns>The valid expense ID entered by the user, or -1 if the input is invalid.</returns>
        public static int GetExpenseId(List<TransactionDetail> expenseDetails) {
            Console.Write("\nEnter Id: ");
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out int id);

            if (isNumber && id < expenseDetails.Count + 1 && id > 0) {
                return id;
            } else if (isNumber) {
                OutputManager.DisplayInvalidInputError($"ID should be between (1 - {expenseDetails.Count + 1})");
            } else {
                OutputManager.DisplayInvalidInputError();
            }
            return -1;
        }

        /// <summary>
        /// Prompts the user to enter a category or source based on the provided type.
        /// </summary>
        /// <param name="type">The type to display in the prompt (e.g., "Category" or "Source").</param>
        /// <returns>The string input entered by the user.</returns>
        public static string GetCategoryOrSource(string type) {
            Console.Write($"Enter {type}: ");
            return Console.ReadLine();
        }
    }
}

