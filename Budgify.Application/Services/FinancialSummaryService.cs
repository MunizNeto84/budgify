using Budgify.Application.Dtos;
using Budgify.Application.Interfaces;

namespace Budgify.Application.Services
{
    public class FinancialSummaryService : IFinancialSummaryService
    {
        public readonly IIncomeRepository _incomeRepository;
        public readonly IExpenseRepository _expenseRepository;

        public FinancialSummaryService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
        }

        public FinancialSummaryDto GetSummary()
        {
            var incomes = _incomeRepository.GetAll();
            var expenses = _expenseRepository.GetAll();

            decimal totalIncome = incomes.Sum(i => i.Amount);
            decimal totalExpense = expenses.Sum(e => e.Amount);
            return new FinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = totalIncome - totalExpense
            };
        }
    }
}
