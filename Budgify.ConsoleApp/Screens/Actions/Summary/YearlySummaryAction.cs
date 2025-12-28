
using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Summary
{
    public class YearlySummaryAction : BaseScreen, IScreenAction
    {
        private readonly IFinancialSummaryService _financialSummaryService;
        public YearlySummaryAction(IFinancialSummaryService financialSummaryService)
        {
            _financialSummaryService = financialSummaryService;
        }
        public void Execute()
        {
            ShowHeader("📊 Resumo Anual");
           
            int year = ReadInt("Ano (ex: 2025)");

            var summary = _financialSummaryService.GetYearlySummary(year);

            Console.WriteLine($"\n📅 Relatório de {year}");
            Console.WriteLine($"--------------------------------");
            Console.WriteLine($"🟢 Receitas: {summary.TotalIncome:C}");
            Console.WriteLine($"🔴 Despesas: {summary.TotalExpense:C}");
            Console.WriteLine($"--------------------------------");
            if(summary.YearlyBalance >= 0)
            {
                Console.WriteLine($"💰 Resultado: {summary.YearlyBalance:C} (Superávit)");
            } 
            else
            {
                Console.WriteLine($"💸 Resultado: {summary.YearlyBalance:C} (Déficit)");
            }
            WaitUser();
        }
    }
}
