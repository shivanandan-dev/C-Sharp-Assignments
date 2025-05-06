namespace ExpenseTracker.Model {
    internal interface ITransactionDetail {
        decimal Amount { get; set; }
        DateTime Date { get; set; }
    }
}
