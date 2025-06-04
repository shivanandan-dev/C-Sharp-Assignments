namespace BankingSystem {
    internal class BankManager {
        /// <summary>
        /// Handles the execution of menu actions based on user input.
        /// </summary>
        /// <param name="menuActions">
        /// A dictionary where the key is an integer representing the menu option,
        /// and the value is a tuple containing a description (string) and an action (Action) to execute.
        /// </param>
        /// <param name="isInfinite">Whether to loop infinitely or exit after one iteration.</param>
        public static void HandleMenuAction(
            Dictionary<int, (string description, Action action)> menuActions,
            bool isInfinite = true) {
            do {
                Console.Clear();
                DisplayMainMenu("Account Operations", menuActions);
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && menuActions.ContainsKey(choice)) {
                    menuActions[choice].action.Invoke();
                } else {
                    Console.WriteLine("[Error] Invalid input.");
                }

                PromptForContinuation();
            } while (isInfinite);
        }

        /// <summary>
        /// Handles the operations menu for a given bank account, such as deposit, withdrawal, or balance display.
        /// </summary>
        /// <param name="account">The bank account on which operations are performed.</param>
        public static void HandleAccountOperations(BankAccount account) {
            Dictionary<int, (string, Action action)> accountOperationMenuAction = new Dictionary<int, (string, Action)>() {
                { 1, ("Deposit", () => DepositAmount(account)) },
                { 2, ("Withdraw", () => Withdraw(account)) },
                { 3, ("Balance", () => account.DisplayBalance()) },
                { 4, ("Exit", () => Environment.Exit(0)) }
            };

            HandleMenuAction(accountOperationMenuAction);
        }

        /// <summary>
        /// Initializes the account based on user input.
        /// </summary>
        /// <param name="accountFactory">
        /// A factory function that takes (string accountNumber, decimal initialBalance)
        /// and returns a new BankAccount instance (e.g., new SavingsAccount(...) or new CheckingAccount(...)).
        /// </param>
        public static BankAccount InitializeAccount(Func<string, decimal, BankAccount> accountFactory) {
            Console.Clear();
            Console.WriteLine("===== Account Details =====\n");

            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            string input = Console.ReadLine();
            bool isNumber = decimal.TryParse(input, out decimal balance);

            if (!isNumber || balance < 0) {
                Console.WriteLine("[Error] Invalid balance. Please restart the application.");
                return null;
            }

            return accountFactory(accountNumber, balance);
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        public static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Displays the account operations menu to the user.
        /// </summary>
        /// <param name="menuTitle">
        /// The title of the menu to be displayed. This is shown as a header above the menu options.
        /// </param>
        /// <param name="menuActions">
        /// A dictionary where the key is an integer representing the menu option,
        /// and the value is a tuple containing a description (string) of the menu option
        /// and an action (Action) to be performed when the option is selected.
        /// </param>
        public static void DisplayMainMenu(
            string menuTitle,
            Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menuTitle} =====\n");
            foreach (var entry in menuActions) {
                Console.WriteLine($"{entry.Key}. {entry.Value.description}");
            }
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Prompts the user to enter a positive decimal amount. Repeats until valid.
        /// </summary>
        /// <param name="prompt">The message to display when asking for input (e.g., "Enter amount to deposit: ").</param>
        /// <returns>A valid, positive decimal value.</returns>
        private static decimal GetDecimal(string prompt) {
            while (true) {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (decimal.TryParse(input, out decimal amount) && amount > 0) {
                    return amount;
                }

                Console.WriteLine("[Error] Invalid input. Please enter a positive number.\n");
            }
        }

        /// <summary>
        /// Handles the deposit operation for the given account by asking for a valid decimal amount.
        /// </summary>
        /// <param name="account">The bank account to deposit into.</param>
        private static void DepositAmount(BankAccount account) {
            decimal amount = GetDecimal("Enter amount to deposit: ");
            account.Deposit(amount);
        }

        /// <summary>
        /// Handles the withdrawal operation for the given account by asking for a valid decimal amount.
        /// </summary>
        /// <param name="account">The bank account to withdraw from.</param>
        private static void Withdraw(BankAccount account) {
            decimal amount = GetDecimal("Enter amount to withdraw: ");
            account.Withdraw(amount);
        }
    }
}
