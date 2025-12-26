using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Application.Services
{
    public class CreditCardService : ICreditCardService
    {
        public readonly ICreditCardRepository _repository;
        public readonly IAccountRepository _accountRepository;
        public readonly IExpenseRepository _expensesRepository;

        public CreditCardService(ICreditCardRepository repository, IAccountRepository accountRepository, IExpenseRepository expenseRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
            _expensesRepository = expenseRepository;
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

        public void PayInvoice(Guid cardId)
        {

            var card = _repository.GetById(cardId);
            if (card == null) throw new Exception("Cartão não encontrado");

            DateTime paymentCutoff = DateTime.Now;

            var account = _accountRepository.GetById(card.AccountId);

            var expenses = _expensesRepository.GetUnpaidExpensesByCard(cardId, paymentCutoff);
            if (expenses.Count == 0) throw new Exception("❌ Nenhuma fatura aberta ou vencida para pagar!");

            decimal totalInvoice = expenses.Sum(e => e.Amount);

            account?.Withdraw(totalInvoice);
            _accountRepository.Update(account!);

            card.RestoreLimit(totalInvoice);
            _repository.Update(card);

            foreach(var expense in expenses)
            {
                expense.Paid = true;
            }

        }


        public List<CreditCard> GetByAccountId(Guid accountId)
        {
            return _repository.GetByAccountId(accountId);
        }
    }
}
