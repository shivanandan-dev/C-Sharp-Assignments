namespace BankingSystem {
    public class SavingsAccount : BankAccount {
        public decimal MinimumBalance = 500m;

        /// <summary>
        /// Initializes a new instance of the class with the specified account number and initial balance.
        /// </summary>
        /// <param name="accountNumber">The account number of the savings account.</param>
        /// <param name="balance">The initial balance of the savings account.</param>
        public SavingsAccount(string accountNumber, decimal balance) : base(accountNumber, balance) { }

        /// <summary>
        /// Withdraws the specified amount from the savings account while ensuring the minimum balance requirement is met.
        /// </summary>
        /// <param name="amount">The amount to withdraw. Must be a positive value and leave the account with at least the minimum balance.</param>
        public override void Withdraw(decimal amount) {
            if (amount > 0 && (Balance - amount) >= MinimumBalance) {
                Balance -= amount;
                Console.WriteLine($"[Success] Amount withdrawn. Available Balance: {Balance}");
            } else {
                Console.WriteLine("[Error] Insufficient balance or Invalid input.");
            }
        }
    }
}