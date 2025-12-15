using Budgify.Domain.Enums;

namespace Budgify.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
