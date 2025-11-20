namespace Budgify.Domain
{
    public class Income
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public IncomeCategory Category { get; set; } 
        public string IncomeType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}