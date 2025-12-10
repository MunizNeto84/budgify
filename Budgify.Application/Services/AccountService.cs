using Budgify.Application.Interfaces;
using Budgify.Domain.Entities;

namespace Budgify.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void CreateAccount(string accountName, decimal initialBalance)
        {
            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Name = accountName,
                Balance = initialBalance,
            };

            _repository.Add(newAccount);
        }

        public List<Account> GetAllAccounts()
        {
            return _repository.GetAll();
        }
    }
}
