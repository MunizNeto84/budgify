using Budgify.Domain;

namespace Budgify.Application;

public interface IAccountRepository
{
    void Add(Account account);

    IEnumerable<Account> GetAll();
}