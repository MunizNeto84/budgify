using Budgify.Domain;

namespace Budgify.Application
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;

        public IncomeService (IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }

        public void CreateIncome(DateTime date, IncomeCategory category, string incomeType, decimal amount)
        {
            var newIncome = new Income
            {
                Id = Guid.NewGuid(),
                Date = date,
                Category = category,
                IncomeType = incomeType,
                Amount = amount
            };
            _incomeRepository.Add(newIncome);
        }
    }
}
