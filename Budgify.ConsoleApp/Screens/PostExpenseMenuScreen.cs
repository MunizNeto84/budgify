using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Screens.Actions.Expense;

namespace Budgify.ConsoleApp.Screens
{
    public class PostExpenseMenuScreen: BaseScreen
    {
        private readonly IAccountService _accountService;
        private readonly IExpenseService _expenseService;
        private readonly ICreditCardService _creditCardService;
        public PostExpenseMenuScreen(IAccountService accountService, IExpenseService expenseService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _expenseService = expenseService;
            _creditCardService = creditCardService;
        }

        public void Show()
        {
            int option = -1;
            do
            {
                ShowHeader("💸 Lançar despesa");
                Console.WriteLine("1 - 💲 Debito");
                Console.WriteLine("2 - 💳 Crédito");
                Console.WriteLine("\n0 - ↪️ Voltar");
                option = ReadInt("Opção");
                switch (option)
                {
                    case 1:
                        new CreateExpenseAction(_accountService, _expenseService).Execute();
                        break;
                    case 2:
                        new CreateExpenseCardAction(_accountService, _expenseService, _creditCardService).Execute();
                        break;
                    case 0:
                        Console.WriteLine("Voltando...");
                        WaitUser();
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        WaitUser();
                        break;
                }
            } while (option != 0);

            }
    }
}
