using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Infrastructure.Memory
{
    public class InMemoryAccountRepository: IAccountRepository
    {
        private static readonly List<Account> _accounts = new List<Account>();

        public void Add(Account account)
        {
            _accounts.Add(account);
        }

        public List<Account> GetAll()
        {
            return _accounts;
        }
    }
}
