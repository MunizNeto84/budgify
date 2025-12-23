using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        public readonly IExpenseRepository _repository;
        public readonly IAccountRepository _accountRepository;

        public ExpenseService(IExpenseRepository repository, IAccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
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

        public List<Expense> GetAllExpenses()
        {
            return _repository.GetAll();
        }
    }
}
