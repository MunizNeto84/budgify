using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface ICreditCardService
    {
        public void CreateCreditCard(Guid accountId, string name, decimal limit, int closingDay, int dueDay);

        public void PayInvoice(Guid cardId);

        List<CreditCard> GetByAccountId(Guid accountId);
    }
}
