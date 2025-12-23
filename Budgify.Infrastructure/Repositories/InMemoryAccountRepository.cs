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

        public Account? GetById(Guid id)
        {
            return _accounts.FirstOrDefault(acc => acc.Id == id);
        }

        public void Update(Account account)
        {
            var index = _accounts.FindIndex(acc => acc.Id == account.Id);
            if (index != -1)
            {
                _accounts[index] = account;
            }
        }

        public List<Account> GetAll()
        {
            return _accounts;
        }
    }
}
