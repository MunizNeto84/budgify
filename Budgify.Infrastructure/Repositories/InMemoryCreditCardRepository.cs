using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Infrastructure.Repositories
{
    public class InMemoryCreditCardRepository: ICreditCardRepository
    {
        private static readonly List<CreditCard> _cards = new List<CreditCard>();

        public void Add(CreditCard card)
        {
            _cards.Add(card);
        }

        public List<CreditCard> GetByAccountId(Guid accountId)
        {
            return _cards.Where(card => card.AccountId == accountId).ToList(); 
        }

        public CreditCard? GetById(Guid id)
        {
            return _cards.FirstOrDefault(card => card.Id == id);
        }

        public List<CreditCard> GetAll()
        {
            return _cards;
        }

        public void Update(CreditCard creditCard)
        {
            var index = _cards.FindIndex(card => card.Id == creditCard.Id);
            if (index != -1)
            {
                _cards[index] = creditCard;
            }
        }
    }
}
