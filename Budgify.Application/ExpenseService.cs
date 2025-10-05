using Budgify.Domain;

namespace Budgify.Application
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void CreateExpense(DateTime date, string category, string expenseType, decimal amount)
        {
            var newExpense = new Expense
            {
                Id = Guid.NewGuid(),
                Date = date,
                Category = category,
                ExpenseType = expenseType,
                Amount = amount
            };

            _expenseRepository.Add(newExpense);
        }
    }
}
