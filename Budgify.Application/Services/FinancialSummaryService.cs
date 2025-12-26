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
            decimal totalExpense = expenses.Where(e => e.Paid).Sum(e => e.Amount);
            decimal totalPaidCard =  expenses.Where(e => e.CreditCardId != null && e.Paid).Sum(e => e.Amount);
            decimal totalPendingCard = expenses.Where(e => e.CreditCardId != null && !e.Paid).Sum(e => e.Amount);
            decimal currentBalance = account.Sum(a => a.Balance);

            return new FinancialSummaryDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                TotalPaidCreditCard = totalPaidCard,
                TotalPendingCreditCard = totalPendingCard,
                Balance = currentBalance
            };
        }
    }
}
