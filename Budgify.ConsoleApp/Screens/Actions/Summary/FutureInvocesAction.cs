using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Summary
{
    public class FutureInvocesAction: BaseScreen, IScreenAction
    {
        private readonly IFinancialSummaryService _summaryService;

        public FutureInvocesAction(IFinancialSummaryService summaryService)
        {
            _summaryService = summaryService;
        }
        public void Execute()
        {
            ShowHeader("📊 Faturas Futura");
            var invoices = _summaryService.GetFutureInvoices();

            if (invoices.Count == 0)
            {
                Console.WriteLine("✅ Nenhuma fatura futura pendente.");
            }
            else
            {
                foreach( var invoice in invoices)
                {
                    Console.WriteLine($"📅 {invoice.MonthYear}: {invoice.Amount:C}");
                }
            }
            WaitUser();
        }
    }
}
