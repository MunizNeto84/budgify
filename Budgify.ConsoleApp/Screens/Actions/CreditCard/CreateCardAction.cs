using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.CreditCard
{
    public class CreateCardAction: BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;
        private readonly ICreditCardService _creditCardService;

        public CreateCardAction(IAccountService accountService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _creditCardService = creditCardService;
        }
        public void Execute()
        {
            ShowHeader("💳 Adicionar Cartão");
            var accounts = _accountService.GetAllAccounts();
            
            if (accounts.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma conta criada no momento.");
                return;
            }

            Console.WriteLine("🏦 Vincular a qual conta?");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i} - {accounts[i].Bank}");
            }
            int index = ReadInt("🔢 Digite a opção");
            if(index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }

            var selectAccount = accounts[index];

            string name = ReadString("💳 Apelido do Cartão");
            decimal limit = ReadDecimal("💲Limite");
            int closing = ReadInt("📅 Dia do fechamento");
            int due = ReadInt("📅 Dia do vencimento");

            _creditCardService.CreateCreditCard(selectAccount.Id, name, limit, closing, due);
            Console.WriteLine("\n✅ Cartão criado com sucesso.");

            WaitUser();
        }
    }
}
