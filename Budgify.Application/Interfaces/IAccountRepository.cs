using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface IAccountRepository
    {
        public void Add(Account account);

        List<Account> GetAll();

        Account? GetById(Guid id);

        public void Update(Account account);
    }
}
