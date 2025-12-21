using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;
using System.Security.Principal;

namespace Budgify.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void CreateAccount(BankName bankName, decimal initialBalance, AccountType type)
        {
            Account newAccount;


            if (type == AccountType.Checking)
            {
                newAccount = new CheckingAccount
                {
                    Id = Guid.NewGuid(),
                    Bank = bankName,
                    Balance = initialBalance
                };
            } else
            {
                newAccount = new InventimentAccount
                {
                    Id = Guid.NewGuid(),
                    Bank = bankName,
                    Balance = initialBalance
                };
            }

                _repository.Add(newAccount);
        }

        public List<Account> GetAllAccounts()
        {
            return _repository.GetAll();
        }
    }
}
