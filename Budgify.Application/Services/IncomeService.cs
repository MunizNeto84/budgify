using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Services
{
    public class IncomeService : IIncomeService
    {
        public readonly IIncomeRepository _repository;

        public IncomeService(IIncomeRepository repository)
        {
            _repository = repository; 
        }

        public void CreateIncome(Guid accountId, decimal amount, DateTime date, IncomeCategory category, string description)
        {
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
