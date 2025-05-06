using ExpenseTracker.IOManager;
using ExpenseTracker.Model;

namespace ExpenseTracker.AppInteraction {
    internal class ExpenseManager {
        static List<ExpenseDetail> ExpenseDetails = new List<ExpenseDetail>();

        public static void AddTransaction() {
            do {
                Console.Clear();
                Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                    { 1, ("Add Expense", ExpenseManager.AddExpense)},
                    { 2, ("Add Income", () => { })},
                    { 3, ("Main Menu", () => { })}
                };

                OutputManager.DisplayMenu("Add Transaction", menuActions);
                int choice = InputManager.GetMenuChoice(menuActions);
                if (choice == 3) {
                    return;
                }
                menuActions[choice].action.Invoke();

                InputManager.PromptForContinuation();
            } while (true);
        }

        public static void AddDefaultData() {
            ExpenseDetails.Add(new ExpenseDetail(50.75M, new DateTime(2025, 1, 15), "Groceries"));
            ExpenseDetails.Add(new ExpenseDetail(120.00M, new DateTime(2025, 2, 10), "Utilities"));
            ExpenseDetails.Add(new ExpenseDetail(45.30M, new DateTime(2025, 3, 5), "Transport"));
            ExpenseDetails.Add(new ExpenseDetail(200.00M, new DateTime(2025, 4, 20), "Dining Out"));
            ExpenseDetails.Add(new ExpenseDetail(80.00M, new DateTime(2025, 5, 1), "Leisure"));
        }

        static void AddExpense() {
            Console.Clear();
            Console.WriteLine("===== New Expense =====\n");
            decimal amount = InputManager.GetAmount();
            DateTime date = InputManager.GetDate();
            string category = InputManager.GetCategoryOrSource("Category");

            ExpenseDetails.Add(new ExpenseDetail(amount, date, category));
            OutputManager.DisplaySuccessMessage("New expense added.");
        }

        public static void ViewTransactions() {
            do {
                Console.Clear();
                Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                    { 1, ("View Expenses", ExpenseManager.ViewExpenses )},
                    { 2, ("View Incomes", () => { })},
                    { 3, ("Main Menu", () => { })}
                };

                OutputManager.DisplayMenu("View Transactions", menuActions);
                int choice = InputManager.GetMenuChoice(menuActions);
                if (choice == 3) {
                    return;
                }
                menuActions[choice].action.Invoke();

                InputManager.PromptForContinuation();
            } while (true);
        }

        static void ViewExpenses() {
            Console.Clear();
            Console.WriteLine("===== Expenses =====\n");
            if (ExpenseDetails.Count == 0) {
                Console.WriteLine("[Error] Empty list.");
            } else {
                OutputManager.DisplayExpenses(ExpenseDetails);
            }
        }
    }
}
