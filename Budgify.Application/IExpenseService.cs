using Budgify.Domain;

namespace Budgify.Application
{
    public interface IExpenseService
    {
        void CreateExpense(DateTime date, string category, string expenseType, decimal amount);
    }
}
