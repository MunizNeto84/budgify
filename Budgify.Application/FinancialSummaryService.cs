using Budgify.Domain;
using System.Linq;

namespace Budgify.Application
{
    public class FinancialSummaryService : IFinancialSummaryService
    {
        private readonly IIncomeRepository _incomeRepo;
        private readonly IExpenseRepository _expenseRepo;

        public FinancialSummaryService(IIncomeRepository incomeRepo, IExpenseRepository expenseRepo)
        {
            _incomeRepo = incomeRepo;
            _expenseRepo = expenseRepo;
        }

        public FinancialSummaryDto GetSummary()
        {
            var allIncomes = _incomeRepo.GetAll();
            var allExpenses = _expenseRepo.GetAll();

            var totalIncome = allIncomes.Sum(Income => Income.Amount);
            var totalExpenses = allExpenses.Sum(Expenses => Expenses.Amount);
            var finalBalance = totalIncome - totalExpenses;

            return new FinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                FinalBalance = finalBalance
            };
        }
    }
}
