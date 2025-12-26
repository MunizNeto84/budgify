using Budgify.Application.Interfaces;
using Budgify.ConsoleApp.Entities;
using Budgify.ConsoleApp.Interfaces;

namespace Budgify.ConsoleApp.Screens.Actions.Expense
{
    public class ListExpensesAction: BaseScreen, IScreenAction
    {
        private readonly IExpenseService _expenseService;

        public ListExpensesAction(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public void Execute()
        {
            ShowHeader("📋 Listar despesas lançadas:");
            var expenses = _expenseService.GetAllExpenses();
            if (expenses.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma despesa lançada no momento.");
            }
            else
            {
                foreach(var expense in expenses)
                {
                    Console.WriteLine($"{expense.Date.ToShortDateString()} | {expense.Description} | {expense.Amount:C} | ({expense.Category})");
                }
                WaitUser();
            }
        }
    }
}
