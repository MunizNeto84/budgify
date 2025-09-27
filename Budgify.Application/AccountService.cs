using Budgify.Domain;

namespace Budgify.Application;


public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void CreateAccount(string bankName, decimal initialBalance, bool hasCreditCard)
    {
        var newAccount = new Account
        {
            Id = Guid.NewGuid(),
            BankName = bankName,
            InitialBalance = initialBalance,
            HasCreditCard = hasCreditCard
        };

        _accountRepository.Add(newAccount);
    }

}
