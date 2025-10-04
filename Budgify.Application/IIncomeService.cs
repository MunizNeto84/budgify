using Budgify.Domain;

namespace Budgify.Application
{
    public interface IIncomeService
    {
        void CreateIncome(DateTime date, string category, string incomeType, decimal amount );
    }
}
