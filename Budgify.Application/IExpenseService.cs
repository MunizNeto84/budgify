using Budgify.Domain;

namespace Budgify.Application
{
    public interface IExpenseService
    {
        void CreateExpense(DateTime date, ExpenseCategory category, string expenseType, decimal amount);
    }
}
