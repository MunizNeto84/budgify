using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface ICreditCardService
    {
        public void CreateCreditCard(Guid accountId, string name, decimal limit, int closingDay, int dueDay);

        List<CreditCard> GetByAccountId(Guid accountId);
    }
}
