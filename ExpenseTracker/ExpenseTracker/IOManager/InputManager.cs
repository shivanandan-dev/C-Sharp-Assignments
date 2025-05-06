using ExpenseTracker.Utils;

namespace ExpenseTracker.IOManager {
    internal class InputManager {
        public static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static int GetMenuChoice(Dictionary<int, (string, Action)> menuActions) {
            do {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);
                if (isNumber && menuActions.ContainsKey(choice)) {
                    return choice;
                } else {
                    OutputManager.DisplayInvalidChoiceError();
                }
            } while (true);
        }

        public static decimal GetAmount() {
            do {
                Console.Write("Enter Amount: ");
                string input = Console.ReadLine();
                bool isDecimal = decimal.TryParse(input, out decimal amount);

                if (isDecimal && amount > 0) {
                    return amount;
                } else if (isDecimal) {
                    OutputManager.DisplayNegativeAmountError();
                } else {
                    OutputManager.DisplayInvalidInputError();
                }
            } while (true);
        }

        public static DateTime GetDate() {
            do {
                Console.Write("Enter Date (dd/mm/yyyy): ");
                string input = Console.ReadLine();
                bool isValidDate = Validator.IsValidDateFormat(input);

                if (isValidDate) {
                    return DateTime.Parse(input);
                } else {
                    OutputManager.DisplayInvalidInputError();
                }
            } while (true);
        }

        public static string GetCategoryOrSource(string type) {
            Console.Write($"Enter {type}: ");
            return Console.ReadLine();
        }
    }
}

