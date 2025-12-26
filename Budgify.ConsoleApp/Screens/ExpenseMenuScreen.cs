using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Screens.Actions.CreditCard;
using Budgify.ConsoleApp.Screens.Actions.Expense;


namespace Budgify.ConsoleApp.Screens
{
    public class ExpenseMenuScreen: BaseScreen
    {
        private readonly IAccountService _accountService;
        private readonly IExpenseService _expenseService;
        private readonly ICreditCardService _creditCardService;
        public ExpenseMenuScreen(IAccountService accountService, IExpenseService expenseService, ICreditCardService creditCardService)
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
                ShowHeader("💸 Depesas");
                Console.WriteLine("1 - 💲 Lançar despesa");
                Console.WriteLine("2 - 💳 Pagar fatura do cartão");
                Console.WriteLine("3 - 📋 Listar despesa(s)");
                Console.WriteLine("\n0 - ↪️ Voltar");
                option = ReadInt("Opção");
                switch (option)
                {
                    case 1:
                        new PostExpenseMenuScreen(_accountService, _expenseService, _creditCardService).Show();
                        break;
                    case 2:
                        new PayInvoiceAction(_accountService, _creditCardService).Execute();
                        break;
                    case 3:
                        new ListExpensesAction(_expenseService).Execute();
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
