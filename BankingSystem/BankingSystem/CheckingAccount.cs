namespace BankingSystem {
    public class CheckingAccount : BankAccount {
        /// <summary>
        /// Initializes a new instance of the class with the specified account number and initial balance.
        /// </summary>
        /// <param name="accountNumber">The account number of the savings account.</param>
        /// <param name="balance">The initial balance of the savings account.</param>
        public CheckingAccount(string accountNumber, decimal balance) : base(accountNumber, balance) { }
    }
}
