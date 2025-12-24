using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Interfaces
{
    public interface IExpenseService
    {
        public void CreateExpense(Guid accountId, decimal amount, DateTime date, ExpenseCategory category, string description);
        public void CreateCardExpense(Guid cardId, decimal amount, int installments, ExpenseCategory category, string description);

        List<Expense> GetAllExpenses();
    }
}
