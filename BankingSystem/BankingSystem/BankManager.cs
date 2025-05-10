namespace BankingSystem {
    internal class BankManager {
        /// <summary>
        /// Handles the execution of menu actions based on user input.
        /// </summary>
        /// <param name="menuActions">
        /// A dictionary where the key is an integer representing the menu option, 
        /// and the value is a tuple containing a description (string) and an action (Action) to execute.
        /// </param>
        public static void HandleMenuAction(Dictionary<int, (string, Action action)> menuActions) {
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
            } while (true);
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
        /// <param name="account">The bank account to initialize.</param>
        /// <param name="accountType">The type of account to initialize (SavingsAccount or CheckingAccount).</param>
        public static void InitializeAccount(ref BankAccount account, Type accountType) {
            Console.Clear();
            Console.WriteLine("===== Account Details =====\n");

            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            string input = Console.ReadLine();
            bool isNumber = decimal.TryParse(input, out decimal balance);

            if (balance < 0 || !isNumber) {
                Console.WriteLine("[Error] Invalid balance. Please restart the application.");
                return;
            }

            if (accountType == typeof(SavingsAccount)) {
                account = new SavingsAccount(accountNumber, balance);
            } else if (accountType == typeof(CheckingAccount)) {
                account = new CheckingAccount(accountNumber, balance);
            }
        }

        /// <summary>
        /// Handles a generic transaction (e.g., deposit or withdrawal) by validating user input and performing the transaction.
        /// </summary>
        /// <param name="account">The bank account on which the transaction is performed.</param>
        /// <param name="transactionAction">The action to perform (deposit or withdraw).</param>
        /// <param name="transactionType">The type of transaction (e.g., "deposit" or "withdraw").</param>
        public static void HandleTransaction(BankAccount account, Action<BankAccount, decimal> transactionAction, string transactionType) {
            Console.Write($"Enter amount to {transactionType}: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal amount) && amount > 0) {
                transactionAction(account, amount);
            } else {
                Console.WriteLine("[Error] Invalid input. Please enter a positive number.");
            }
        }

        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Handles the deposit operation for the given account.
        /// </summary>
        /// <param name="account">The bank account to deposit into.</param>
        static void DepositAmount(BankAccount account) {
            HandleTransaction(account, (acc, amount) => acc.Deposit(amount), "deposit");
        }

        /// <summary>
        /// Handles the withdrawal operation for the given account.
        /// </summary>
        /// <param name="account">The bank account to withdraw from.</param>
        static void Withdraw(BankAccount account) {
            HandleTransaction(account, (acc, amount) => acc.Withdraw(amount), "withdraw");
        }

        /// <summary>
        /// Displays the account operations menu to the user.
        /// </summary>
        /// <param name="menutitle">
        /// The title of the menu to be displayed. This is shown as a header above the menu options.
        /// </param>
        /// <param name="menuActions">
        /// A dictionary where the key is an integer representing the menu option, 
        /// and the value is a tuple containing a description (string) of the menu option
        /// and an action (Action) to be performed when the option is selected.
        /// </param>
        static void DisplayMainMenu(string menutitle, Dictionary<int, (string description, Action)> menuActions) {
            Console.WriteLine($"===== {menutitle} =====\n");
            foreach (var menuAction in menuActions) {
                Console.WriteLine($"{menuAction.Key}. {menuAction.Value.description}");
            }
            Console.Write("\n[Menu] Enter your choice: ");
        }
    }
}