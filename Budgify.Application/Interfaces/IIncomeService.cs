using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Interfaces
{
    public interface IIncomeService
    {
        public void CreateIncome(Guid accountId, decimal amount, DateTime date, IncomeCategory category, string description);
        List<Income> GetAllIncomes();
    }
}
