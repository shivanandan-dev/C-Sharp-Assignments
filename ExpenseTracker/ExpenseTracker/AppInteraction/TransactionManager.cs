using ExpenseTracker.IOManager;
using ExpenseTracker.Model;

namespace ExpenseTracker.AppInteraction {
    internal class TransactionManager {
        static List<TransactionDetail> ExpenseDetails = new List<TransactionDetail>();
        static List<TransactionDetail> IncomeDetails = new List<TransactionDetail>();

        const string CategoryLabel = "Category";
        const string SourceLabel = "Source";
        const string ExpenseLabel = "Expense";
        const string IncomeLabel = "Income";

        /// <summary>
        /// Adds default expense and income data to their respective lists.
        /// </summary>
        public static void AddDefaultData() {
            ExpenseDetails.Add(new TransactionDetail(50.75M, new DateTime(2025, 1, 15), "Groceries"));
            ExpenseDetails.Add(new TransactionDetail(120.00M, new DateTime(2025, 2, 10), "Utilities"));
            ExpenseDetails.Add(new TransactionDetail(45.30M, new DateTime(2025, 3, 5), "Transport"));
            ExpenseDetails.Add(new TransactionDetail(200.00M, new DateTime(2025, 4, 20), "Dining Out"));
            ExpenseDetails.Add(new TransactionDetail(80.00M, new DateTime(2025, 5, 1), "Leisure"));

            IncomeDetails.Add(new TransactionDetail(1000.00M, new DateTime(2025, 1, 1), "Salary"));
            IncomeDetails.Add(new TransactionDetail(200.00M, new DateTime(2025, 2, 10), "Freelancing"));
            IncomeDetails.Add(new TransactionDetail(10000.00M, new DateTime(2025, 1, 1), "Youtube Channel"));
            IncomeDetails.Add(new TransactionDetail(200.90M, new DateTime(2025, 2, 10), "Gaming"));
        }

        /// <summary>
        /// Displays a menu to add a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void AddTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Add Expense", () => HandleAddTransaction(ExpenseDetails, ExpenseLabel, CategoryLabel)) },
                { 2, ("Add Income", () => HandleAddTransaction(IncomeDetails, IncomeLabel, SourceLabel)) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Add Transaction", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Displays a menu to view transactions (expenses or incomes) and performs the selected action.
        /// </summary>
        public static void ViewTransactions() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("View Expenses", () => HandleViewTransaction(ExpenseDetails, ExpenseLabel, CategoryLabel)) },
                { 2, ("View Incomes", () => HandleViewTransaction(IncomeDetails, IncomeLabel, SourceLabel)) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("View Transactions", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Displays a menu to delete a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void DeleteTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Delete Expense", () => HandleDeleteTransaction(ExpenseDetails, ExpenseLabel)) },
                { 2, ("Delete Income", () => HandleDeleteTransaction(IncomeDetails, IncomeLabel)) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Delete Transaction", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Displays a menu to edit a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void EditTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Edit Expense", () => HandleEditTransaction(ExpenseDetails, ExpenseLabel)) },
                { 2, ("Edit Income", () => HandleEditTransaction(IncomeDetails, IncomeLabel))},
                { 3, ("Main Menu", () => { }) }
             };

            Application.DisplayMenuWithActions("Edit Transaction", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Calculates and displays a financial summary including total expenses, total incomes, and net balance.
        /// </summary>
        public static void ViewFinancialSummary() {
            decimal totalExpense = 0M;
            decimal totalIncome = 0M;
            foreach (TransactionDetail expenseDetail in ExpenseDetails) {
                totalExpense += expenseDetail.Amount;
            }
            foreach (TransactionDetail incomeDetails in IncomeDetails) {
                totalIncome += incomeDetails.Amount;
            }

            OutputManager.DisplayFinancialSummary(totalExpense, totalIncome);
        }

        /// <summary>
        /// Prompts the user to add a new transaction (expense or income) and adds it to the specified details list.
        /// </summary>
        /// <param name="details">The list of <see cref="TransactionDetail"/> to which the new transaction will be added.</param>
        /// <param name="header">The title displayed in the prompt, e.g. "Expense" or "Income".</param>
        /// <param name="label">The label for the additional information field, e.g. "Category" or "Source".</param>
        private static void HandleAddTransaction(
            List<TransactionDetail> details,
            string header,
            string label
        ) {
            Console.Clear();
            Console.WriteLine($"===== New {header} =====\n");
            decimal amount = InputManager.GetAmount();
            DateTime date = InputManager.GetDate();
            string additionalInformation = InputManager.GetCategoryOrSource(label);

            details.Add(new TransactionDetail(amount, date, additionalInformation));
            OutputManager.DisplaySuccessMessage($"New {header.ToLower()} added.");
        }

        /// <summary>
        /// Displays all transactions (expenses or incomes) from the specified list.
        /// </summary>
        /// <param name="transactionDetails">The list of <see cref="TransactionDetail"/> to display.</param>
        /// <param name="header">The title displayed in the header, e.g. "Expense" or "Income".</param>
        /// <param name="label">The label for the additional information field, e.g. "Category" or "Source".</param>
        private static void HandleViewTransaction(
            List<TransactionDetail> transactionDetails,
            string header,
            string label
        ) {
            Console.Clear();
            Console.WriteLine($"===== {header}s =====\n");

            if (transactionDetails.Count == 0) {
                Console.WriteLine("[Error] Empty list.");
                return;
            }

            OutputManager.DisplayTransaction(transactionDetails, label);
        }

        /// <summary>
        /// Deletes a specific transaction (expense or income) from the specified list.
        /// </summary>
        /// <param name="transactionDetails">The list of <see cref="TransactionDetail"/> from which to delete.</param>
        /// <param name="header">The title displayed in the header, e.g. "Expense" or "Income".</param>
        private static void HandleDeleteTransaction(
            List<TransactionDetail> transactionDetails,
            string header
        ) {
            Console.Clear();

            // Display current transactions for selection
            if (header == ExpenseLabel)
                HandleViewTransaction(ExpenseDetails, ExpenseLabel, CategoryLabel);
            else
                HandleViewTransaction(IncomeDetails, IncomeLabel, SourceLabel);

            if (transactionDetails.Count == 0)
                return;

            Console.WriteLine("\n===== Delete =====\n");
            int transactionId = InputManager.GetTransactionId(transactionDetails);
            if (transactionId > 0 && transactionId <= transactionDetails.Count) {
                transactionDetails.RemoveAt(transactionId - 1);
                OutputManager.DisplaySuccessMessage($"{header} deleted.");
            }
        }

        /// <summary>
        /// Allows the user to edit a specific transaction (expense or income) in the specified list.
        /// </summary>
        /// <param name="transactionDetails">The list of <see cref="TransactionDetail"/> containing the transaction to edit.</param>
        /// <param name="header">The title displayed in the header, e.g. "Expense" or "Income".</param>
        private static void HandleEditTransaction(
            List<TransactionDetail> transactionDetails,
            string header
        ) {
            Console.Clear();

            // Display current transactions for selection
            if (header == ExpenseLabel)
                HandleViewTransaction(ExpenseDetails, ExpenseLabel, CategoryLabel);
            else
                HandleViewTransaction(IncomeDetails, IncomeLabel, SourceLabel);

            if (transactionDetails.Count == 0)
                return;

            Console.WriteLine("\n===== Edit =====\n");
            int transactionId = InputManager.GetTransactionId(transactionDetails);
            if (transactionId > 0 && transactionId <= transactionDetails.Count) {
                EditTransactionAt(transactionId - 1, transactionDetails);
            }
        }

        /// <summary>
        /// Displays a menu to edit specific attributes of a transaction (amount, date, or category/source).
        /// </summary>
        /// <param name="transactionId">The ID of the transaction to edit.</param>
        /// <param name="transactionList">The list of transactions (expenses or incomes).</param>
        private static void EditTransactionAt(
            int transactionId,
            List<TransactionDetail> transactionList
        ) {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>(){
                { 1, ("Edit Amount", () => UpdateTransactionAmount(transactionId, transactionList, InputManager.GetAmount)) },
                { 2, ("Edit Date", () => UpdateTransactionDate(transactionId, transactionList, InputManager.GetDate)) },
                { 3, ("Edit Category/Source", () => UpdateTransactionCategory(transactionId, transactionList, InputManager.GetCategoryOrSource)) },
                { 4, ("Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Edit Transaction", menuActions, menuActions.Count, false, false, false);
        }

        /// <summary>
        /// Updates the amount of a specific transaction.
        /// </summary>
        /// <param name="transactionId">The zero-based index of the transaction to update.</param>
        /// <param name="transactionList">The list containing the transaction to update.</param>
        /// <param name="action">A function that returns the new amount.</param>
        private static void UpdateTransactionAmount(
            int transactionId,
            List<TransactionDetail> transactionList,
            Func<decimal> action
        ) {
            decimal updatedValue = action.Invoke();
            transactionList[transactionId].Amount = updatedValue;
            OutputManager.DisplaySuccessMessage("Amount updated.");
        }

        /// <summary>
        /// Updates the date of a specific transaction.
        /// </summary>
        /// <param name="transactionId">The zero-based index of the transaction to update.</param>
        /// <param name="transactionList">The list containing the transaction to update.</param>
        /// <param name="action">A function that returns the new date.</param>
        private static void UpdateTransactionDate(
            int transactionId,
            List<TransactionDetail> transactionList,
            Func<DateTime> action
        ) {
            DateTime updatedValue = action.Invoke();
            transactionList[transactionId].Date = updatedValue;
            OutputManager.DisplaySuccessMessage("Date updated.");
        }

        /// <summary>
        /// Updates the category or source of a specific transaction.
        /// </summary>
        /// <param name="transactionId">The zero-based index of the transaction to update.</param>
        /// <param name="transactionList">The list containing the transaction to update.</param>
        /// <param name="action">A function that takes a prompt and returns the new category or source string.</param>
        private static void UpdateTransactionCategory(
            int transactionId,
            List<TransactionDetail> transactionList,
            Func<string, string> action
        ) {
            string updatedValue = action.Invoke("Category/Source");
            transactionList[transactionId].AdditionalInformation = updatedValue;
            OutputManager.DisplaySuccessMessage("Category/Source updated.");
        }
    }
}
