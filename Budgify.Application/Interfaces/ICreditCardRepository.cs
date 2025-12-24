using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface ICreditCardRepository
    {
        public void Add(CreditCard card);

        List<CreditCard> GetByAccountId(Guid accountId);

        CreditCard? GetById(Guid id);

        public void Update(CreditCard card);
    }
}
