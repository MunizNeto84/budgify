using Budgify.Application; 
using Budgify.Domain;

namespace Budgify.Infrastructure;

public class AccountRepository : IAccountRepository
{
    private readonly BudgifyDbContext _context;

    public AccountRepository(BudgifyDbContext context)
    {
        _context = context;
    }

    public void Add(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }
}