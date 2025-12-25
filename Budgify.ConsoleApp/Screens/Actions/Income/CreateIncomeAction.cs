using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;
using Budgify.Domain.Enums;

namespace Budgify.ConsoleApp.Screens.Actions.Income
{
    public class CreateIncomeAction : BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;

        public CreateIncomeAction(IAccountService accountService, IIncomeService incomeService)
        {
            _accountService = accountService;
            _incomeService = incomeService;
        }

        public void Execute()
        {
            ShowHeader("💲 Lançar Receita");
            var accounts = _accountService.GetAllAccounts();

            if (accounts.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma conta criada no momento.");
                return;
            }

            Console.WriteLine("🏦 Para qual conta é a receita?");
            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i} - {accounts[i].Bank}");
            }
            int index = ReadInt("🔢 Digite a opção");
            if (index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }

            var selectAccount = accounts[index];

            string description = ReadString("Descrição");
            decimal amount = ReadDecimal("Valor");
            var category = ReadEnum<IncomeCategory>("Selecione a categoria");

            _incomeService.CreateIncome(selectAccount.Id, amount, DateTime.Now, category, description);

            Console.WriteLine("\n✅ Receita lançada no sistema com sucesso.");
            WaitUser();
        }
    }
}
