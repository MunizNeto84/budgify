using Budgify.Application.Dtos;
using Budgify.Application.Interfaces;

namespace Budgify.Application.Services
{
    public class FinancialSummaryService : IFinancialSummaryService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICreditCardRepository _creditCardRepository;

        public FinancialSummaryService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, IAccountRepository accountRepository, ICreditCardRepository creditCardRepository)
        {
            _incomeRepository = incomeRepository;
            _expenseRepository = expenseRepository;
            _accountRepository = accountRepository;
            _creditCardRepository = creditCardRepository;
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

        public MonthlySummaryDto GetMonthlySummary(int month, int year)
        {
            var allIncomes = _incomeRepository.GetAll();
            var allExpenses = _expenseRepository.GetAll();
            var allCards = _creditCardRepository.GetAll();

            var monthlyIncome = allIncomes.Where(i => i.Date.Month == month && i.Date.Year == year).Sum(i => i.Amount);
            decimal monthlyExpense = 0;

            foreach (var expense in allExpenses)
            {
                if (expense.CreditCardId == null)
                {
                    if (expense.Date.Month == month && expense.Date.Year == year)
                    {
                        monthlyExpense += expense.Amount;
                    }
                }
                else
                {
                    var card = allCards.FirstOrDefault(c => c.Id == expense.CreditCardId);
                    if (card != null)
                    {
                        DateTime invoiceDate = expense.Date;

                        if (expense.Date.Day > card.ClosingDay)
                        {
                            invoiceDate = expense.Date.AddMonths(1);
                        }

                        if (expense.Date.Month == month && invoiceDate.Year == year)
                        {
                            monthlyExpense += expense.Amount;
                        }
                    }
                }
            }

            return new MonthlySummaryDto
            {
                Month = month,
                Year = year,
                TotalIncome = monthlyIncome,
                TotalExpense = monthlyExpense,
                MonthlyBalance = monthlyIncome - monthlyExpense
            };
        }

        public YearlySummaryDto GetYearlySummary(int year)
        {
            var allIncomes = _incomeRepository.GetAll();
            var allExpenses = _expenseRepository.GetAll();
            var allCards = _creditCardRepository.GetAll();

            var yearlyIncome = allIncomes.Where(i => i.Date.Year == year).Sum(i => i.Amount);
            decimal yearlyExpense = 0;

            foreach (var expense in allExpenses)
            {
                if (expense.CreditCardId == null)
                {
                    if (expense.Date.Year == year)
                    {
                        yearlyExpense += expense.Amount;
                    }
                }
                else
                {
                    var card = allCards.FirstOrDefault(c => c.Id == expense.CreditCardId);
                    if (card != null)
                    {
                        DateTime invoiceDate = expense.Date;

                        if (expense.Date.Day > card.ClosingDay)
                        {
                            invoiceDate = expense.Date.AddMonths(1);
                        }

                        if (expense.Date.Year == year && invoiceDate.Year == year)
                        {
                            yearlyExpense += expense.Amount;
                        }
                    }
                }
            }

            return new YearlySummaryDto
            {
                
                Year = year,
                TotalIncome = yearlyIncome,
                TotalExpense = yearlyExpense,
                YearlyBalance = yearlyIncome - yearlyExpense
            };
        }

        public List<FutureInvoiceDto> GetFutureInvoices()
        {
            var expenses = _expenseRepository.GetAll()
                .Where(e => e.CreditCardId != null && !e.Paid)
                .ToList();

            var cards = _creditCardRepository.GetAll();
            var invoiceGroups = new List<(DateTime Date, decimal Amount)>();

            foreach(var expense in expenses)
            {
                var card = cards.FirstOrDefault(c => c.Id == expense.CreditCardId);
                if (card == null) continue;

                DateTime invoiceDate = expense.Date;
                if (expense.Date.Day > card.ClosingDay)
                {
                    invoiceDate = expense.Date.AddMonths(1);
                }

                DateTime groupKey = new DateTime(invoiceDate.Year, invoiceDate.Month, 1);

                invoiceGroups.Add((groupKey, expense.Amount));
            }

            var report = invoiceGroups
                .GroupBy(x => x.Date)
                .Select(g => new FutureInvoiceDto
                {
                    MonthYear = g.Key.ToString("MM/yyyy"),
                    Amount = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.MonthYear)
                .ToList();

            return report;
        }
    }
}
