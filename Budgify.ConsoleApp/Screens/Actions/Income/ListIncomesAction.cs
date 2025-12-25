using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Income
{
    public class ListIncomesAction: BaseScreen, IScreenAction
    {
        private readonly IIncomeService _incomeService;

        public ListIncomesAction(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        public void Execute()
        {
            ShowHeader("📋 Listar receitas lançadas:");
            var incomes = _incomeService.GetAllIncomes();
            if(incomes.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma receita lançada no momento.");
            }
            else
            {
                foreach(var income in incomes)
                {
                    Console.WriteLine($"💲 {income.Date.ToShortDateString()} | {income.Description} | {income.Amount:C} | ({income.Category})");
                }
                WaitUser();
            }
        }
    }
}
