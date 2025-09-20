namespace Budgify.Domain
{
    public class Expense
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Category  { get; set; }
        public string ExpenseType {  get; set; }
        public decimal Amount { get; set; }
    }
}
