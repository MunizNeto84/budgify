namespace Budgify.Domain
{
    public class Income
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string IncomeType { get; set; }
        public decimal Amount { get; set; }
    }
}
