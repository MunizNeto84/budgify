using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface IExpenseRepository
    {
        public void Add(Expense expense);
        List<Expense> GetAll();
    }
}
