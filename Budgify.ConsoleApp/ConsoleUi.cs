using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Screens;
using Budgify.Domain.Entities;
using Budgify.Domain.Enums;


namespace Budgify.ConsoleApp
{
    public class ConsoleUi
    {
        private readonly IAccountService _accountService;
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;
        private readonly IFinancialSummaryService _financialSummaryService;
        private readonly ICreditCardService _creditCardService;


        public ConsoleUi(IAccountService accountService, IIncomeService incomeService, IExpenseService expenseService, IFinancialSummaryService financialSummaryService, ICreditCardService creditCardService)
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
                ShowMenu();
                Console.Write("\nDigite uma opção: ");
                var input = Console.ReadLine()!;
                if(!int.TryParse(input, out option))
                {
                    option = -1;
                }

                Console.Clear();

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
                        WaitUser();
                        break;
                    case 4:
                        var summary = _financialSummaryService.GetSummary();
                        Console.WriteLine("Resumo:");
                        Console.WriteLine($"Total Receitas: {summary.TotalIncome:C}");
                        Console.WriteLine($"Total Despesas: {summary.TotalExpense:C}");
                        Console.WriteLine();

                        if(summary.Balance >= 0)
                        {
                            Console.WriteLine($"Saldo final: {summary.Balance:C} ✅");
                        } else
                        {
                            Console.WriteLine($"Saldo final: {summary.Balance:C} 🚨");
                        }

                        WaitUser();
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


        private void ShowMenu()
        {
            Console.WriteLine("===============================================");
            Console.WriteLine("=================== BUDGIFY ===================");
            Console.WriteLine("===============================================");
            Console.WriteLine("1 - Conta");
            Console.WriteLine("2 - Receita");
            Console.WriteLine("3 - Despesa");
            Console.WriteLine("4 - Resumo\n");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("===============================================");
        }

         

       

        
        private void WaitUser()
        {
            Console.WriteLine("\nPressione ENTER para voltar ao menu...");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
