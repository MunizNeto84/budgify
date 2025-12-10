using Budgify.Domain.Entities;

namespace Budgify.Application.Interfaces
{
    public interface IAccountService
    {
        public void CreateAccount(string Name, decimal initialBalance);

        List<Account> GetAllAccounts();
    }
}
