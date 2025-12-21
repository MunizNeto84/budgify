using Budgify.Domain.Entities;
using Budgify.Domain.Enums;

namespace Budgify.Application.Interfaces
{
    public interface IAccountService
    {
        public void CreateAccount(BankName bankName, decimal initialBalance, AccountType type);

        List<Account> GetAllAccounts();
    }
}
