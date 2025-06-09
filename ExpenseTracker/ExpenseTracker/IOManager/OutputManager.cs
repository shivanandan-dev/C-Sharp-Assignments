using ExpenseTracker.Model;

namespace ExpenseTracker.IOManager {
    internal class OutputManager {
        /// <summary>
        /// Displays a menu with a title and a list of menu options.
        /// </summary>
        /// <param name="menuTitle">The title of the menu to display.</param>
        /// <param name="menuActions">A dictionary of menu options, where each key represents the menu option number and the value contains a description and an associated action.</param>
        public static void DisplayMenu(
            string menuTitle,
            Dictionary<int, (string description, Action)> menuActions
        ) {
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
        /// Displays a list of transactions in a table with columns for ID, amount, date, and a custom type.
        /// </summary>
        /// <param name="expenseDetails">The collection of <see cref="TransactionDetail"/> items to render.</param>
        /// <param name="type">The column header label for the AdditionalInformation field (e.g. "Category" or "Source").</param>
        public static void DisplayTransaction(
            List<TransactionDetail> expenseDetails,
            string type
        ) {
            Console.WriteLine("{0, -5} | {1, -15} | {2, -10} | {3, -20}", "Id", "Amount", "Date", $"{type}");
            Console.WriteLine(new string('-', 60));

            int count = 1;
            foreach (TransactionDetail expenseDetail in expenseDetails) {
                Console.WriteLine(
                    "{0,-5} | {1,-15} | {2,-10} | {3,-20}",
                    count++,
                    expenseDetail.Amount,
                    expenseDetail.Date.ToString("dd/MM/yyyy"),
                    expenseDetail.AdditionalInformation
                );
            }
        }

        /// <summary>
        /// Displays a financial summary, including total incomes, total expenses, and the net balance.
        /// </summary>
        /// <param name="totalExpenses">The total amount of expenses.</param>
        /// <param name="totalIncomes">The total amount of incomes.</param>
        public static void DisplayFinancialSummary(
            decimal totalExpenses,
            decimal totalIncomes
        ) {
            Console.WriteLine("\n{0, -11} : {1, -10}", "Income", totalIncomes);
            Console.WriteLine("{0, -11} : {1, -10}", "Expences", totalExpenses);
            Console.WriteLine("{0, -11} : {1, -10}", "Net balance", totalIncomes - totalExpenses);
        }
    }
}
