using ExpenseTracker.Model;
using ExpenseTracker.Utils;

namespace ExpenseTracker.IOManager {
    internal class InputManager {
        public static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static int GetMenuChoice(Dictionary<int, (string, Action)> menuActions) {
            Console.WriteLine();
            do {
                Console.Write($"[Menu] Enter choice: ");
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);
                if (isNumber && menuActions.ContainsKey(choice)) {
                    return choice;
                } else {
                    OutputManager.DisplayInvalidInputError("Invalid Choice");
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
                    OutputManager.DisplayInvalidInputError("Amount should be a positive decimal");
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
                    OutputManager.DisplayInvalidInputError("Invalid Date Format");
                }
            } while (true);
        }

        public static int GetExpenseId(List<ExpenseDetail> expenseDetails) {

            Console.Write("Enter Id: ");
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

        public static string GetCategoryOrSource(string type) {
            Console.Write($"Enter {type}: ");
            return Console.ReadLine();
        }
    }
}

