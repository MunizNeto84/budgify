using Budgify.Domain;

namespace Budgify.Application
{
    public interface IIncomeService
    {
        void CreateIncome(DateTime date, IncomeCategory category, string incomeType, decimal amount );
    }
}
