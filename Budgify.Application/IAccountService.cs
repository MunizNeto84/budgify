using Budgify.Domain;

namespace Budgify.Application
{
    public interface IAccountService
    {
        void CreateAccount(string bankName, decimal initialBalance, bool hasCreditCard);
    }
}
