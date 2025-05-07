using ExpenseTracker.IOManager;

namespace ExpenseTracker.AppInteraction {
    internal class Application {

        /// <summary>
        /// Displays a menu and performs the action based on the user's selection.
        /// </summary>
        /// <param name="title">The title of the menu to display.</param>
        /// <param name="menuActions">A dictionary mapping menu options to their respective descriptions and actions.</param>
        /// <param name="exitOption">The menu option that terminates the loop and exits.</param>
        /// <param name="clearConsole">Whether to clear the console before displaying the menu.</param>
        /// <param name="repeat">Whether to repeat the menu after an action is performed.</param>
        /// <param name="promptForContinuation">Whether to prompt for continuation after each action.</param>
        public static void DisplayMenuWithActions(string title, Dictionary<int, (string, Action action)> menuActions, int exitOption = 0, bool clearConsole = true, bool repeat = true, bool promptForContinuation = true) {
            do {
                Console.WriteLine();
                if (clearConsole)
                    Console.Clear();
                OutputManager.DisplayMenu(title, menuActions);
                int choice = InputManager.GetMenuChoice(menuActions);
                if (choice == exitOption) {
                    return;
                }
                menuActions[choice].action.Invoke();
                if (promptForContinuation)
                    InputManager.PromptForContinuation();
            } while (repeat);
        }

        /// <summary>
        /// Displays the main menu of the application and handles user interaction with various options, such as adding, viewing, editing, and deleting transactions, 
        /// as well as generating a financial summary or exiting the application.
        /// </summary>
        public static void HandleMainMenu() {
            Dictionary<int, (string, Action action)> menuActions = new Dictionary<int, (string, Action)>() {
                    { 1, ("Add Transaction", TransactionManager.AddTransaction )},
                    { 2, ("View Transactions", TransactionManager.ViewTransactions )},
                    { 3, ("Edit Transactions", TransactionManager.EditTransaction )},
                    { 4, ("Delete Transactions", TransactionManager.DeleteTransaction )},
                    { 5, ("Financial Summary", TransactionManager.ViewFinancialSummary )},
                    { 6, ("Exit", () => { Environment.Exit(0); })}
                };

            DisplayMenuWithActions("Main Menu", menuActions);
        }
    }
}
