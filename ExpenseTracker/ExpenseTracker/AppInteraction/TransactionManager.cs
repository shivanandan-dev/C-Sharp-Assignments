using ExpenseTracker.IOManager;
using ExpenseTracker.Model;

namespace ExpenseTracker.AppInteraction {
    internal class TransactionManager {
        static List<TransactionDetail> ExpenseDetails = new List<TransactionDetail>();
        static List<TransactionDetail> IncomeDetails = new List<TransactionDetail>();

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
                { 1, ("Add Expense", TransactionManager.AddExpense) },
                { 2, ("Add Income", TransactionManager.AddIncome) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Add Transaction", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Displays a menu to view transactions (expenses or incomes) and performs the selected action.
        /// </summary>
        public static void ViewTransactions() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("View Expenses", TransactionManager.ViewExpenses) },
                { 2, ("View Incomes", TransactionManager.ViewIncomes) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("View Transactions", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Displays a menu to delete a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void DeleteTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Delete Expense", TransactionManager.DeleteExpense) },
                { 2, ("Delete Income", TransactionManager.DeleteIncome) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Delete Transaction", menuActions, menuActions.Count);
        }

        /// <summary>
        /// Displays a menu to edit a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void EditTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Edit Expense", TransactionManager.EditExpense) },
                { 2, ("Edit Income", TransactionManager.EditIncome) },
                { 3, ("Main Menu", () => { }) }
             };

            Application.DisplayMenuWithActions("Edit Transaction", menuActions, menuActions.Count);
        }


        /// <summary>
        /// Calculates and displays a financial summary including total expenses, total incomes, and net balance.
        /// </summary>
        public static void ViewFinancialSummary() {
            Console.Clear();
            Console.WriteLine("===== Financial Summary =====");
            decimal totalExpense = 0M;
            decimal totalIncome = 0M;
            foreach (TransactionDetail incomeDetail in IncomeDetails) {
                totalExpense += incomeDetail.Amount;
            }
            foreach (TransactionDetail expenseDetail in ExpenseDetails) {
                totalIncome += expenseDetail.Amount;
            }

            OutputManager.DisplayFinancialSummary(totalExpense, totalIncome);
        }


        /// <summary>
        /// Prompts the user to add a new expense and adds it to the ExpenseDetails list.
        /// </summary>
        private static void AddExpense() {
            Console.Clear();
            Console.WriteLine("===== New Expense =====\n");
            decimal amount = InputManager.GetAmount();
            DateTime date = InputManager.GetDate();
            string additionalInformation = InputManager.GetCategoryOrSource("Category");

            ExpenseDetails.Add(new TransactionDetail(amount, date, additionalInformation));
            OutputManager.DisplaySuccessMessage("New expense added.");
        }

        /// <summary>
        /// Prompts the user to add a new income and adds it to the IncomeDetails list.
        /// </summary>
        private static void AddIncome() {
            Console.Clear();
            Console.WriteLine("===== New Income =====\n");
            decimal amount = InputManager.GetAmount();
            DateTime date = InputManager.GetDate();
            string source = InputManager.GetCategoryOrSource("Source");

            IncomeDetails.Add(new TransactionDetail(amount, date, source));
            OutputManager.DisplaySuccessMessage("New income added.");
        }

        /// <summary>
        /// Displays all expenses from the ExpenseDetails list.
        /// </summary>
        private static void ViewExpenses() {
            Console.Clear();
            Console.WriteLine("===== Expenses =====\n");
            if (ExpenseDetails.Count == 0) {
                Console.WriteLine("[Error] Empty list.");
                return;
            }

            OutputManager.DisplayTransaction(ExpenseDetails, "Category");
        }

        /// <summary>
        /// Displays all incomes from the IncomeDetails list.
        /// </summary>
        private static void ViewIncomes() {
            Console.Clear();
            Console.WriteLine("===== Incomes =====\n");
            if (IncomeDetails.Count == 0) {
                Console.WriteLine("[Error] Empty list.");
                return;
            }

            OutputManager.DisplayTransaction(IncomeDetails, "Source");
        }


        /// <summary>
        /// Deletes a specific expense from the ExpenseDetails list.
        /// </summary>
        private static void DeleteExpense() {
            Console.Clear();
            ViewExpenses();
            if (ExpenseDetails.Count == 0)
                return;

            Console.WriteLine("\n===== Delete =====\n");
            int expenseId = InputManager.GetExpenseId(ExpenseDetails);
            if (expenseId > 0) {
                ExpenseDetails.RemoveAt(expenseId - 1);
                OutputManager.DisplaySuccessMessage("Expense deleted.");
            }
        }

        /// <summary>
        /// Deletes a specific income from the IncomeDetails list.
        /// </summary>
        private static void DeleteIncome() {
            Console.Clear();
            ViewIncomes();
            if (IncomeDetails.Count == 0)
                return;

            Console.WriteLine("\n===== Delete =====\n");
            int incomeId = InputManager.GetExpenseId(IncomeDetails);
            if (incomeId > 0) {
                IncomeDetails.RemoveAt(incomeId - 1);
                OutputManager.DisplaySuccessMessage("Income deleted.");
            }
        }

        /// <summary>
        /// Allows the user to edit a specific expense from the ExpenseDetails list.
        /// </summary>
        private static void EditExpense() {
            Console.Clear();
            ViewExpenses();
            if (ExpenseDetails.Count == 0)
                return;

            Console.WriteLine("\n===== Delete =====\n");
            int expenseId = InputManager.GetExpenseId(ExpenseDetails);
            if (expenseId > 0) {
                EditTransactionAt(expenseId - 1, ExpenseDetails);
            }
        }

        /// <summary>
        /// Allows the user to edit a specific income from the IncomeDetails list.
        /// </summary>
        private static void EditIncome() {
            Console.Clear();
            ViewIncomes();
            if (IncomeDetails.Count == 0)
                return;

            int incomeId = InputManager.GetExpenseId(IncomeDetails);
            if (incomeId > 0) {
                EditTransactionAt(incomeId - 1, IncomeDetails);
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

            Application.DisplayMenuWithActions("Edit Transaction", menuActions, 4, false, false, false);
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
