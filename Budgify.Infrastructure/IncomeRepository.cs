using Budgify.Application;
using Budgify.Domain;

namespace Budgify.Infrastructure;

public class IncomeRepository : IIncomeRepository
{
    private readonly BudgifyDbContext _context;

    public IEnumerable<Income> GetAll()
    {
        return _context.Incomes.ToList();
    }

    public IncomeRepository(BudgifyDbContext context)
    {
        _context = context;
    }

    public void Add(Income income)
    {
        _context.Incomes.Add(income);
        _context.SaveChanges();
    }
}
