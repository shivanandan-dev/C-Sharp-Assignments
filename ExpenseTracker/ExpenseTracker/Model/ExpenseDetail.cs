namespace ExpenseTracker.Model {
    internal class ExpenseDetail : ITransactionDetail {
        /// <summary>
        /// Gets or sets the amount of the expense.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date of the expense.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the category of the expense.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseDetail"/> class with the specified amount, date, and category.
        /// </summary>
        /// <param name="amount">The amount of the expense.</param>
        /// <param name="date">The date of the expense.</param>
        /// <param name="category">The category of the expense.</param>
        public ExpenseDetail(decimal amount, DateTime date, string category) {
            Amount = amount;
            Date = date;
            Category = category;
        }
    }
}
