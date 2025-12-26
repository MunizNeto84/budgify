using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;
using Budgify.Domain.Enums;

namespace Budgify.ConsoleApp.Screens.Actions.Expense
{
    public class CreateExpenseAction: BaseScreen, IScreenAction
    {
        private readonly IAccountService _accountService;
        private readonly IExpenseService _expenseService;

        public CreateExpenseAction(IAccountService accountService, IExpenseService expenseService)
        {
            _accountService = accountService;
            _expenseService = expenseService;
        }

        public void Execute()
        {
            
            var accounts = _accountService.GetAllAccounts();
            if(accounts.Count == 0)
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
            if (index < 0 || index >= accounts.Count)
            {
                Console.WriteLine("❌ Índice inválido.");
                WaitUser();
                return;
            }

            var selectAccount = accounts[index];
            string description = ReadString("Descrição");
            decimal amount = ReadDecimal("Valor");
            var category = ReadEnum<ExpenseCategory>("Selecione a categoria");
            _expenseService.CreateExpense(selectAccount.Id, amount, DateTime.Now, category, description);
            
            Console.WriteLine("\n✅ Despesa lançada com sucesso.");
            WaitUser();
        }
    }
}
