using Budgify.Application.Dtos;
using Budgify.Application.Interfaces;

namespace Budgify.Application.Services
{
    public class FinancialSummaryService : IFinancialSummaryService
    {
        public readonly IIncomeRepository _incomeRepository;
        public readonly IExpenseRepository _expenseRepository;
        public readonly IAccountRepository _accountRepository;

        public FinancialSummaryService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, IAccountRepository accountRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _accountRepository = accountRepository;
        }

        public FinancialSummaryDto GetSummary()
        {
            var incomes = _incomeRepository.GetAll();
            var expenses = _expenseRepository.GetAll();
            var account = _accountRepository.GetAll();

            decimal totalIncome = incomes.Sum(i => i.Amount);
            decimal totalExpense = expenses.Sum(e => e.Amount);

            decimal currentBalance = account.Sum(a => a.Balance);

            return new FinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = currentBalance
            };
        }
    }
}
