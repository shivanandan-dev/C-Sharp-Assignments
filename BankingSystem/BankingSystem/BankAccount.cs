namespace BankingSystem {
    public class BankAccount {
        /// <summary>
        /// The account number for the user.
        /// </summary>
        /// <value>The account number is a unique identifier for the user's account.</value>
        public string AccountNumber { get; set; }

        /// <summary>
        /// The balance of the account.
        /// </summary>
        /// <value>The balance represents the current amount of money in the user's account.</value>
        public decimal Balance { get; set; }

        /// <summary>
        /// Initializes a new instance of the class with the specified account number and initial balance.
        /// </summary>
        /// <param name="accountNumber">The account number of the bank account.</param>
        /// <param name="balance">The initial balance of the bank account.</param>
        public BankAccount(string accountNumber, decimal balance) {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        /// <summary>
        /// Deposits the specified amount into the bank account.
        /// </summary>
        /// <param name="amount">The amount to deposit. Must be a positive value.</param>
        public virtual void Deposit(decimal amount) {
            if (amount > 0) {
                Balance += amount;
                Console.WriteLine("[Success] Amount deposited.");
            } else {
                Console.WriteLine("[Error] Amount value should be positive.");
            }
        }

        /// <summary>
        /// Withdraws the specified amount from the bank account.
        /// </summary>
        /// <param name="amount">The amount to withdraw. Must be a positive value and less than or equal to the current balance.</param>
        public virtual void Withdraw(decimal amount) {
            if (amount > 0 && amount <= Balance) {
                Balance -= amount;
                Console.WriteLine("[Success] Amount withdrawn.");
            } else {
                Console.WriteLine("[Error] Insufficient balance or Invalid input.");
            }
        }

        /// <summary>
        /// Displays the current balance of the bank account.
        /// </summary>
        public void DisplayBalance() {
            Console.WriteLine($"Balance: {Balance}");
        }
    }
}