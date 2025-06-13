namespace ExpenseTracker.Model {
    internal interface ITransactionDetail {
        /// <summary>
        /// Gets or sets the amount of the transaction or expense.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the transaction or expense.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
