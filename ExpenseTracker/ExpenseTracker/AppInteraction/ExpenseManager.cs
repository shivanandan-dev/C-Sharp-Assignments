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

        static void AddExpense() {
            Console.Clear();
            Console.WriteLine("===== New Expense =====\n");
            decimal amount = InputManager.GetAmount();
            DateTime date = InputManager.GetDate();
            string category = InputManager.GetCategoryOrSource("Category");

            ExpenseDetails.Add(new ExpenseDetail(amount, date, category));
            OutputManager.DisplaySuccessMessage("New expense added.");
        }
    }
}
