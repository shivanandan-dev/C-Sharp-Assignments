namespace ExpenseTracker.Model {
    internal class TransactionDetail : ITransactionDetail {
        /// <summary>
        /// Gets or sets the amount of the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the additional information (category or source) of the transaction.
        /// </summary>
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Initializes a new instance of the class with the specified amount, date, and additional information.
        /// </summary>
        /// <param name="amount">The amount of the transaction.</param>
        /// <param name="date">The date of the transaction.</param>
        /// <param name="additionalInformation">The additional information of the transaction.</param>
        public TransactionDetail(
            decimal amount,
            DateTime date,
            string additionalInformation
        ) {
            Amount = amount;
            Date = date;
            AdditionalInformation = additionalInformation;
        }
    }
}
