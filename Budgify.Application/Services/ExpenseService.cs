using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        public readonly IExpenseRepository _repository;

        public ExpenseService(IExpenseRepository repository)
        {
            _repository = repository; 
        }

        public void CreateExpense(Guid accountId, decimal amount, DateTime date, ExpenseCategory category, string description)
        {
            var newExpense = new Expense
            {
                AccountId = accountId,
                Amount = amount,
                Date = date,
                Category = category,
                Description = description
            };

            _repository.Add(newExpense);
        }

        public List<Expense> GetAllExpenses()
        {
            return _repository.GetAll();
        }
    }
}
