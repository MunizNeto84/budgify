using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;

namespace Budgify.ConsoleApp.Screens
{
    public class MainMenuScreen: BaseScreen
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;
        private readonly IFinancialSummaryService _financialSummaryService;
        private readonly ICreditCardService _creditCardService;

        public MainMenuScreen(IAccountService accountService, IIncomeService incomeService, IExpenseService expenseService, IFinancialSummaryService financialSummaryService, ICreditCardService creditCardService)
        {
            _accountService = accountService;
            _incomeService = incomeService;
            _expenseService = expenseService;
            _financialSummaryService = financialSummaryService;
            _creditCardService = creditCardService;
        }

        public void Run()
        {
            int option = -1;

            do
            {
                Console.WriteLine("===============================================");
                Console.WriteLine("================= 💰 BUDGIFY ==================");
                Console.WriteLine("===============================================");
                Console.WriteLine("1 - 👥 Conta");
                Console.WriteLine("2 - 💲 Receita");
                Console.WriteLine("3 - 💸 Despesa");
                Console.WriteLine("4 - 📊 Resumo\n");
                Console.WriteLine("0 - ⬅️ Sair");
                Console.WriteLine("===============================================");

                option = ReadInt("🔢 Digite uma opção");

                switch (option)
                {
                    case 1:
                        new AccountMenuScreen(_accountService, _creditCardService).Show();
                        break;
                    case 2:
                        new IncomeMenuScreen(_accountService, _incomeService).Show();
                        break;
                    case 3:
                        new ExpenseMenuScreen(_accountService, _expenseService, _creditCardService).Show();
                        break;
                    case 4:
                        new SummaryMenuScreen(_financialSummaryService).Show();
                        break;
                    case 0:
                        Console.WriteLine("Saindo [...]");
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
