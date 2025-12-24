using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        public readonly IExpenseRepository _repository;
        public readonly IAccountRepository _accountRepository;
        public readonly ICreditCardRepository _creditCardRepository;

        public ExpenseService(IExpenseRepository repository, IAccountRepository accountRepository, ICreditCardRepository creditCardRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
            _creditCardRepository = creditCardRepository;
        }

        public void CreateExpense(Guid accountId, decimal amount, DateTime date, ExpenseCategory category, string description)
        {

            var account = _accountRepository.GetById(accountId);
            if (account == null)
            {
                throw new Exception("Não há conta cadastrada. Impossivel vincular receita.");
            }

            account.Withdraw(amount);
            _accountRepository.Update(account);

            var newExpense = new Expense
            {
                AccountId = accountId,
                Amount = amount,
                Date = date,
                Category = category,
                Description = description
            };

            _repository.Add(newExpense);
        }

        public void CreateCardExpense(Guid cardId, decimal amount, int installments, ExpenseCategory category, string description)
        {
            var card = _creditCardRepository.GetById(cardId);
            if (card == null)
            {
                throw new Exception("Cartão não encontrado");
            }

            card.Purchase(amount);
            _creditCardRepository.Update(card);

            decimal installmentValue = amount / installments;

            for (int i = 0; i < installments; i++)
            {
                var expense = new Expense
                {
                    Id = Guid.NewGuid(),
                    AccountId = card.AccountId,
                    CreditCardId = card.Id,
                    Amount = installmentValue,
                    Date = DateTime.Now.AddMonths(i),
                    Category = category,
                    Description = $"{description} ({i + 1}/{installments})",
                    CurrentInstallment = i + 1,
                    TotalInstallments = installments
                };

                _repository.Add(expense);
            }
        }

        public List<Expense> GetAllExpenses()
        {
            return _repository.GetAll();
        }
    }
}
