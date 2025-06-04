using ExpenseTracker.IOManager;
using ExpenseTracker.Model;

namespace ExpenseTracker.AppInteraction {
    internal class ExpenseManager {
        static List<ExpenseDetail> ExpenseDetails = new List<ExpenseDetail>();

        /// <summary>
        /// Adds default expense data to the ExpenseDetails list.
        /// </summary>
        public static void AddDefaultData() {
            ExpenseDetails.Add(new ExpenseDetail(50.75M, new DateTime(2025, 1, 15), "Groceries"));
            ExpenseDetails.Add(new ExpenseDetail(120.00M, new DateTime(2025, 2, 10), "Utilities"));
            ExpenseDetails.Add(new ExpenseDetail(45.30M, new DateTime(2025, 3, 5), "Transport"));
            ExpenseDetails.Add(new ExpenseDetail(200.00M, new DateTime(2025, 4, 20), "Dining Out"));
            ExpenseDetails.Add(new ExpenseDetail(80.00M, new DateTime(2025, 5, 1), "Leisure"));
        }

        /// <summary>
        /// Displays a menu to add a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void AddTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Add Expense", ExpenseManager.AddExpense) },
                { 2, ("Add Income", () => { }) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Add Transaction", menuActions, 3);
        }  

        /// <summary>
        /// Displays a menu to view transactions (expenses or incomes) and performs the selected action.
        /// </summary>
        public static void ViewTransactions() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("View Expenses", ExpenseManager.ViewExpenses) },
                { 2, ("View Incomes", () => { }) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("View Transactions", menuActions, 3);
        } 

        /// <summary>
        /// Displays a menu to delete a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void DeleteTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Delete Expense", ExpenseManager.DeleteExpense) },
                { 2, ("Delete Income", () => { }) },
                { 3, ("Main Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Delete Transaction", menuActions, 3);
        }    

        /// <summary>
        /// Displays a menu to edit a transaction (expense or income) and performs the selected action.
        /// </summary>
        public static void EditTransaction() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                { 1, ("Edit Expense", ExpenseManager.EditExpense) },
                { 2, ("Edit Income", () => { }) },
                { 3, ("Main Menu", () => { }) }
             };

            Application.DisplayMenuWithActions("Edit Transaction", menuActions, 3);
        }

        /// <summary>
        /// Prompts the user to add a new expense and adds it to the ExpenseDetails list.
        /// </summary>
        private static void AddExpense() {
            Console.Clear();
            Console.WriteLine("===== New Expense =====\n");
            decimal amount = InputManager.GetAmount();
            DateTime date = InputManager.GetDate();
            string category = InputManager.GetCategoryOrSource("Category");

            ExpenseDetails.Add(new ExpenseDetail(amount, date, category));
            OutputManager.DisplaySuccessMessage("New expense added.");
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
            } else {
                OutputManager.DisplayExpenses(ExpenseDetails);
            }
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
                OutputManager.DisplaySuccessMessage("Expense deleted");
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
            int expenseId = InputManager.GetExpenseId(ExpenseDetails);

            if (expenseId > 0) {
                EditExpenseAt(expenseId - 1);
            }
        }


        /// <summary>
        /// Updates the amount of a specific expense.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to update.</param>
        /// <param name="action">A function to retrieve the updated amount.</param>
        private static void UpdateExpenseAmount(int expenseId, Func<decimal> action) {
            decimal updatedValue = action.Invoke();
            ExpenseDetails[expenseId].Amount = updatedValue;
            OutputManager.DisplaySuccessMessage("Amount updated.");
        }

        /// <summary>
        /// Updates the date of a specific expense.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to update.</param>
        /// <param name="action">A function to retrieve the updated date.</param>
        private static void UpdateExpenseDate(int expenseId, Func<DateTime> action) {
            DateTime updatedValue = action.Invoke();
            ExpenseDetails[expenseId].Date = updatedValue;
            OutputManager.DisplaySuccessMessage("Date updated.");
        }

        /// <summary>
        /// Updates the category of a specific expense.
        /// </summary>
        /// <param name="expenseId">The ID of the expense to update.</param>
        /// <param name="action">A function to retrieve the updated category.</param>
        private static void UpdateExpenseCategory(int expenseId, Func<string, string> action) {
            string updatedValue = action.Invoke("Category");
            ExpenseDetails[expenseId].Category = updatedValue;
            OutputManager.DisplaySuccessMessage("Category updated.");
        }

        /// <summary>
        /// Displays a menu to edit specific attributes of an expense (amount, date, or category).
        /// </summary>
        /// <param name="expenseId">The ID of the expense to edit.</param>
        private static void EditExpenseAt(int expenseId) {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>(){
                { 1, ("Edit Amount", () => UpdateExpenseAmount(expenseId, InputManager.GetAmount)) },
                { 2, ("Edit Date", () => UpdateExpenseDate(expenseId, InputManager.GetDate)) },
                { 3, ("Edit Category", () => UpdateExpenseCategory(expenseId, InputManager.GetCategoryOrSource)) },
                { 4, ("Menu", () => { }) }
            };

            Application.DisplayMenuWithActions("Edit Expense", menuActions, 4, false, false, false);
        }
    }
}
