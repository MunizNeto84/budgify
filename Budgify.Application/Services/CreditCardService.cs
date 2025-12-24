using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        public readonly ICreditCardRepository _repository;
        
        public CreditCardService(ICreditCardRepository repository)
        {
            _repository = repository; 
        }

        public void CreateCreditCard(Guid accountId, string name, decimal limit, int closingDay, int dueDay)
        {
            var newCard = new CreditCard
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                Name = name,
                Limit = limit,
                ClosingDay = closingDay,
                DueDay = dueDay,
                AvailableLimit = limit
            };

            _repository.Add(newCard);
        }


        public List<CreditCard> GetByAccountId(Guid accountId)
        {
            return _repository.GetByAccountId(accountId);
        }
    }
}
