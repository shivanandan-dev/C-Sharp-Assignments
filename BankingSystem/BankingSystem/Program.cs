namespace BankingSystem {
    internal class Program {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        static void Main() {
            BankAccount account = null;

            Func<string, decimal, BankAccount> savingsFactory = (number, balance) => new SavingsAccount(number, balance);
            Func<string, decimal, BankAccount> checkingFactory = (number, balance) => new CheckingAccount(number, balance);

            Dictionary<int, (string description, Action action)> accountTypeMenuActions = new Dictionary<int, (string, Action)>()
            {
                { 1, ("Savings Account",    () => { account = BankManager.InitializeAccount(savingsFactory);   }) },
                { 2, ("Checking Account",   () => { account = BankManager.InitializeAccount(checkingFactory);  }) },
                { 3, ("Exit",               () => Environment.Exit(0)) }
            };

            while (account == null) {
                BankManager.HandleMenuAction(accountTypeMenuActions, false);
            }

            if (account != null) {
                BankManager.HandleAccountOperations(account);
            }
        }
    }
}
