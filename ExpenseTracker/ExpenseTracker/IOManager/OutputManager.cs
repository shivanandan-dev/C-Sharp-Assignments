using ExpenseTracker.Model;

namespace ExpenseTracker.IOManager {
    internal class OutputManager {
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menuTitle} =====\n");
            foreach (var menuAction in menuActions)
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
        }

        public static void DisplayInvalidInputError(string errorMessage) {
            Console.WriteLine($"[Error] {errorMessage}.");
        }

        public static void DisplayInvalidInputError() {
            Console.WriteLine("[Error] Invalid input.");
        }

        public static void DisplaySuccessMessage(string message) {
            Console.WriteLine($"[Success] {message}");
        }

        public static void DisplayExpenses(List<ExpenseDetail> expenseDetails) {
            Console.WriteLine("{0, -5} | {1, -15} | {2, -10} | {3, -20}", "Id", "Amount", "Date", "Category");
            Console.WriteLine(new string('-', 60));

            int count = 1;
            foreach (ExpenseDetail expenseDetail in expenseDetails) {
                Console.WriteLine(
                    "{0,-5} | {1,-15} | {2,-10} | {3,-20:C}",
                    count++,
                    expenseDetail.Amount,
                    expenseDetail.Date.ToString("dd/MM/yyyy"),
                    expenseDetail.Category
                );
            }
        }
    }
}
