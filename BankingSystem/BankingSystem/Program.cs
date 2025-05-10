namespace BankingSystem {
    internal class Program {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        static void Main() {
            BankAccount account = null;

            Dictionary<int, (string description, Action action)> accountTypeMenuActions = new Dictionary<int, (string, Action)>()
            {
                { 1, ("Savings Account", () => BankManager.InitializeAccount(ref account, typeof(SavingsAccount))) },
                { 2, ("Checking Account", () => BankManager.InitializeAccount(ref account, typeof(CheckingAccount))) },
                { 3, ("Exit", () => Environment.Exit(0)) }
            };

            BankManager.HandleMenuAction(accountTypeMenuActions);

            if (account != null) {
                BankManager.HandleAccountOperations(account);
            }
        }
    }
}
