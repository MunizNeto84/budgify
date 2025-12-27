
using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Summary
{
    public class MonthlySummaryAction: BaseScreen, IScreenAction
    {
        private readonly IFinancialSummaryService _financialSummaryService;
        public MonthlySummaryAction(IFinancialSummaryService financialSummaryService)
        {
            _financialSummaryService = financialSummaryService;
        }
        public void Execute()
        {
            ShowHeader("📊 Resumo mensal");

            int month = ReadInt("Mês (ex: 1-12)"); 
            int year = ReadInt("Ano (ex: 2025)");

            if (month < 1 || month > 12 || year < 2000)
            {
                Console.WriteLine("Data inválida.");
                WaitUser();
                return;
            }

            var summary = _financialSummaryService.GetMonthlySummary(month, year);

            Console.WriteLine($"\n📅 Relatório de {month}/{year}");
            Console.WriteLine($"--------------------------------");
            Console.WriteLine($"🟢 Receitas: {summary.TotalIncome:C}");
            Console.WriteLine($"🔴 Despesas: {summary.TotalExpense:C}");
            Console.WriteLine($"--------------------------------");
            if(summary.MonthlyBalance >= 0)
            {
                Console.WriteLine($"💰 Resultado: {summary.MonthlyBalance:C} (Superávit)");
            } 
            else
            {
                Console.WriteLine($"💸 Resultado: {summary.MonthlyBalance:C} (Déficit)");
            }
            WaitUser();
        }
    }
}
