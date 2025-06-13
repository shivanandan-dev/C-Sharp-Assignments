namespace ExpenseTracker.Model {
    internal interface ITransactionDetail {
        /// <summary>
        /// Gets or sets the amount of the transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets additional information for the transaction.
        /// </summary>
        public string AdditionalInformation { get; set; }
    }
}
