using Budgify.Domain;

namespace Budgify.Application
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);

        IEnumerable<Expense> GetAll();
    }
}
