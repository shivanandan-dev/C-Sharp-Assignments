namespace BankingSystem {
    internal class BankingApplication {
        /// <summary>
        /// Prompts the user to press any key to continue.
        /// </summary>
        static void PromptForContinuation() {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Handles a generic transaction (e.g., deposit or withdrawal) by validating user input and performing the transaction.
        /// </summary>
        /// <param name="account">The bank account on which the transaction is performed.</param>
        /// <param name="transactionAction">The action to perform (deposit or withdraw).</param>
        /// <param name="transactionType">The type of transaction (e.g., "deposit" or "withdraw").</param>
        static void HandleTransaction(BankAccount account, Action<BankAccount, decimal> transactionAction, string transactionType) {
            Console.Write($"Enter amount to {transactionType}: ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal amount) && amount > 0) {
                transactionAction(account, amount);
            } else {
                Console.WriteLine("[Error] Invalid input. Please enter a positive number.");
            }
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
        /// Displays the account type menu to the user.
        /// </summary>
        static void AccountTypeMenu() {
            Console.WriteLine("===== Account Type =====\n");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Checking Account");
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Displays the account operations menu to the user.
        /// </summary>
        static void AccountOperationsMenu() {
            Console.WriteLine("===== Account Operations =====\n");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Balance");
            Console.WriteLine("4. Exit");
            Console.Write("\n[Menu] Enter your choice: ");
        }

        /// <summary>
        /// Handles the operations menu for a given bank account, such as deposit, withdrawal, or balance display.
        /// </summary>
        /// <param name="account">The bank account on which operations are performed.</param>
        static void HandleAccountOperations(BankAccount account) {
            Dictionary<int, Action> action = new Dictionary<int, Action>() {
                { 1, () => DepositAmount(account) },
                { 2, () => Withdraw(account) },
                { 3, () => account.DisplayBalance() },
                { 4, () => Environment.Exit(0) }
            };

            do {
                Console.Clear();
                AccountOperationsMenu();
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out int choice);

                if (isNumber && action.ContainsKey(choice)) {
                    action[choice]();
                } else {
                    Console.WriteLine("[Error] Invalid input.");
                }

                PromptForContinuation();
            } while (true);
        }

        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        static void Main() {
            BankAccount account = null;

            AccountTypeMenu();
            string input = Console.ReadLine();
            bool isNumber = int.TryParse(input, out int choice);

            if (!isNumber || (choice != 1 && choice != 2)) {
                Console.WriteLine("[Error] Invalid input. Please restart the application.");
                return;
            }

            Console.Clear();
            Console.WriteLine("===== Account Details =====\n");

            Console.Write("Enter Account Number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter Initial Balance: ");
            input = Console.ReadLine();
            isNumber = decimal.TryParse(input, out decimal balance);

            if (balance < 0 || !isNumber) {
                Console.WriteLine("[Error] Invalid balance. Please restart the application.");
                return;
            }

            if (choice == 1) {
                account = new SavingsAccount(accountNumber, balance);
            }

            HandleAccountOperations(account);
        }
    }
}