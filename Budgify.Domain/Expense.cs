namespace Budgify.Domain
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public string ExpenseType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}