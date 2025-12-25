using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Infrastructure.Repositories
{
    public class InMemoryExpenseRepository: IExpenseRepository
    {
        public static readonly List<Expense> _expenses = new List<Expense>();
        public void Add(Expense expense)
        {
            _expenses.Add(expense);
        }

        public List<Expense> GetAll()
        {
            return _expenses; 
        }

        public List<Expense> GetUnpaidExpensesByCard(Guid cardId, DateTime limitDate)
        {
            return _expenses.Where(e => e.CreditCardId == cardId && e.Paid == false && e.Date <= limitDate).ToList();
        }
    }
}
