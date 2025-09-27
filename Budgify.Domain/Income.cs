namespace Budgify.Domain
{
    public class Income
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; } = string.Empty;
        public string IncomeType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}