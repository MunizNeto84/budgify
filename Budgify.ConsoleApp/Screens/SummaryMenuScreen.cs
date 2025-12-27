using Budgify.Application.Interfaces;
using Budgify.Application.Services;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Screens.Actions.Account;
using Budgify.ConsoleApp.Screens.Actions.CreditCard;
using Budgify.ConsoleApp.Screens.Actions.Summary;

namespace Budgify.ConsoleApp.Screens
{
    public class SummaryMenuScreen: BaseScreen
    {
        private readonly IFinancialSummaryService _summaryService;

        public SummaryMenuScreen(IFinancialSummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        public void Show()
        {
            int option = -1;
            do
            {
                ShowHeader("📊 Resumo:");
                Console.WriteLine("1 - 💹 Resumo Geral");
                Console.WriteLine("2 - 📈 Resumo do mês");
                Console.WriteLine("3 - 📈 Resumo do ano");
                Console.WriteLine("4 - 💳 Faturas futuras");
                Console.WriteLine("\n0 - ↪️ Voltar");

                option = ReadInt("Opção");

                switch (option)
                {
                    case 1:
                        new GeneralSummaryAction(_summaryService).Execute();
                        break;
                    case 2:
                        new MonthlySummaryAction(_summaryService).Execute();
                        break;
                    case 3:
                        //
                        break;
                    case 4:
                        //
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
