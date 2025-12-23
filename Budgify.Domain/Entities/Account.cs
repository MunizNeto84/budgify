using Budgify.Domain.Enums;

namespace Budgify.Domain.Entities
{
    public abstract class  Account
    {
        public Guid Id { get; set; }
        public BankName Bank { get; set; }
        public decimal Balance { get; set; }

        public List<CreditCard> Cards { get; set; } = new();
    }
}
