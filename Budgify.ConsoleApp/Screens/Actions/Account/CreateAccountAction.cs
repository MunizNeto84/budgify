
using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;
using Budgify.Domain.Enums;


namespace Budgify.ConsoleApp.Screens.Actions.Account
{
    public class CreateAccountAction : BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;

      

        public CreateAccountAction(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void Execute()
        {
            ShowHeader("✨ Criar conta.");
            var bank = ReadEnum<BankName>("🏦 Selecione o Banco:");
            var type = ReadEnum<AccountType>("👥 Tipo da conta:");
            var balance = ReadDecimal("💵 Saldo Inicial");

            try
            {
                _accountService.CreateAccount(bank, balance, type);
                Console.WriteLine("\n✅ Conta criada com sucesso.");
                WaitUser();

            } catch (Exception ex)
            {
                Console.WriteLine("\n❌ Erro ao criar conta.");
                WaitUser();
            }
        }
    }
}
