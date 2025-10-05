using Budgify.Application;
using Budgify.Domain;

namespace Budgify.Infrastructure
{
    public class ExpenseRepository: IExpenseRepository
    {
        private readonly BudgifyDbContext _context;

        public ExpenseRepository(BudgifyDbContext context) 
        {
            _context = context;
        }

        public void Add(Expense expense)
        { 
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }
    }
}
