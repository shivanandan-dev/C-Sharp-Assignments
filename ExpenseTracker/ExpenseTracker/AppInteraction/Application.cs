using ExpenseTracker.IOManager;

namespace ExpenseTracker.AppInteraction {
    internal class Application {
        public static void HandleMainMenu() {
            do {
                Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                    { 1, ("Add Transaction", ExpenseManager.AddTransaction )},
                    { 2, ("View Transactions", ExpenseManager.ViewTransactions )},
                    { 3, ("Edit Transactions", ExpenseManager.EditTransaction )},
                    { 4, ("Delete Transactions", ExpenseManager.DeleteTransaction )},
                    { 5, ("Financial Summary", () => { })},
                    { 6, ("Exit", () => { Environment.Exit(0); })}
                };

                OutputManager.DisplayMenu("Main Menu", menuActions);

                int choice = InputManager.GetMenuChoice(menuActions);
                menuActions[choice].action.Invoke();
                InputManager.PromptForContinuation();
                Console.Clear();
            } while (true);
        }
    }
}
