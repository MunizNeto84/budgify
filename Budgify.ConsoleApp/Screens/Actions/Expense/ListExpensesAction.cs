using Budgify.Application.Interfaces;
using ExpenseEntity = Budgify.Domain.Entities.Expense;
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
            ShowHeader("📋 Listar despesas");
            var expenses = _expenseService.GetAllExpenses();
            if (expenses.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma despesa lançada no momento.");
                WaitUser();
                return;
            }
            Console.WriteLine("Filtro");
            Console.WriteLine("1- 📉 Todas");
            Console.WriteLine("2- 🔴 Pagas (Saídas efetivas)");
            Console.WriteLine("3- ⌛ Pendentes (Saídas futuras)");
            int filter = ReadInt("Opção");

            var allExpenses = _expenseService.GetAllExpenses();
            List<ExpenseEntity> filteredList = new List<ExpenseEntity>();

            switch (filter)
            {
                case 2: filteredList = allExpenses.Where(e => e.Paid).ToList(); break;
                case 3: filteredList = allExpenses.Where(e => !e.Paid).ToList(); break;
                default: filteredList = allExpenses; break;
            }

            if (filteredList.Count == 0)
            {
                Console.WriteLine("⚠️ Nenhuma despesa encontrada com este filtro.");
                WaitUser();
            }
            else
            {
                Console.WriteLine($"\nExibindo {filteredList.Count} lançamentos:\n");
                foreach(var expense in filteredList)
                {
                    string status = expense.Paid ? "✅ Paga" : "⏳ Pendente";
                    string type = expense.CreditCardId != null ? "💳 Crédito" : "💸 Débito";
                    Console.WriteLine($"{expense.Date.ToShortDateString()} | {expense.Description} | {expense.Amount:C}");
                    Console.WriteLine($"   ↳ {type} | {status} | ({expense.Category})");
                    Console.WriteLine("------------------------------------------------");
                    
                }
                WaitUser();
            }
        }
    }
}
