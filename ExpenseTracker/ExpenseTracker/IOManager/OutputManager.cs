using ExpenseTracker.Model;

namespace ExpenseTracker.IOManager {
    internal class OutputManager {
        /// <summary>
        /// Displays a menu with a title and a list of menu options.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">A dictionary of menu options, where each key represents the menu option number and the value contains a description and an associated action.</param>
        public static void DisplayMenu(string menuTitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menuTitle} =====\n");
            foreach (var menuAction in menuActions)
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
        }

        /// <summary>
        /// Writes an error message to the console, prefixed with “[Error]”.
        /// </summary>
        /// <param name="errorMessage">
        /// The specific error description to display. Defaults to “Invalid input” if none is provided.
        /// </param>
        public static void DisplayInvalidInputError(string errorMessage = "Invalid input") {
            Console.WriteLine($"[Error] {errorMessage}.");
        }

        /// <summary>
        /// Displays a success message when an operation is successfully completed.
        /// </summary>
        /// <param name="message">The success message to display.</param>
        public static void DisplaySuccessMessage(string message) {
            Console.WriteLine($"[Success] {message}");
        }

        /// <summary>
        /// Displays a list of expense details in a tabular format with columns for ID, amount, date, and category.
        /// </summary>
        /// <param name="expenseDetails">A list of <see cref="ExpenseDetail"/> objects containing details about each expense.</param>
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
