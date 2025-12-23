using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Services
{
    public class IncomeService : IIncomeService
    {
        public readonly IIncomeRepository _repository;
        public readonly IAccountRepository _accountRepository;

        public IncomeService(IIncomeRepository repository, IAccountRepository accountRepository)
        {
            _repository = repository;
            _accountRepository = accountRepository;
        }

        public void CreateIncome(Guid accountId, decimal amount, DateTime date, IncomeCategory category, string description)
        {
            var account = _accountRepository.GetById(accountId);
            if (account == null)
            {
                throw new Exception("Não há conta cadastrada. Impossivel vincular receita.");
            }

            account.Deposit(amount);
            _accountRepository.Update(account);

            var newIncome = new Income
            {
                AccountId = accountId,
                Amount = amount,
                Date = date,
                Category = category,
                Description = description
            };

            _repository.Add(newIncome);
        }

        public List<Income> GetAllIncomes()
        {
            return _repository.GetAll();
        }
    }
}
