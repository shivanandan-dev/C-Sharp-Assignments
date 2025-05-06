namespace ExpenseTracker.Model {
    internal class ExpenseDetail : ITransactionDetail {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }

        public ExpenseDetail(decimal amount, DateTime date, string category) {
            Amount = amount;
            Date = date;
            Category = category;
        }
    }
}
