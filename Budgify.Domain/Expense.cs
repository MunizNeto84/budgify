namespace Budgify.Domain
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; } 
        public string ExpenseType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}