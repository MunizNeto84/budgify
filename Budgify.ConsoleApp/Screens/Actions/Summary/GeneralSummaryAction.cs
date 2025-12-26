using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Summary
{
    public class GeneralSummaryAction: BaseScreen, IScreenAction
    {
        private readonly IFinancialSummaryService _financialSummaryService;

        public GeneralSummaryAction(IFinancialSummaryService financialSummaryService)
        {
            _financialSummaryService = financialSummaryService;
        }

        public void Execute()
        {
            var summary = _financialSummaryService.GetSummary();
            ShowHeader("💹 Resumo Geral");
            Console.WriteLine($"🟢 Total de entradas: {summary.TotalIncome:C}");
            Console.WriteLine($"🔴 Total de saidas: {summary.TotalExpense:C}");
            Console.WriteLine($"💳 Faturas Pagas (Cartão): {summary.TotalPaidCreditCard:C}");
            Console.WriteLine($"⏳ Faturas Pendentes:      {summary.TotalPendingCreditCard:C}");
            Console.WriteLine("------------------------------------------\n");

            if ( summary.Balance >= 0)
            {
                Console.WriteLine($"✅ Saldo final: {summary.Balance:C}");
            } else
            {
                Console.WriteLine($"🚨 Saldo final: {summary.Balance:C}");
            }
            WaitUser();
        }
    }
}
