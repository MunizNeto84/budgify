using Budgify.Application.Interfaces;
using Budgify.Application.Services;
using Budgify.ConsoleApp;
using Budgify.Infrastructure.Memory;
using Budgify.Infrastructure.Repositories;

IAccountRepository accountRepository = new InMemoryAccountRepository();
IIncomeRepository incomeRepository = new InMemoryIncomeRepository();
IExpenseRepository expenseRepository = new InMemoryExpenseRepository();

IAccountService accountService = new AccountService(accountRepository);
IIncomeService incomeService = new IncomeService(incomeRepository);
IExpenseService expenseService = new ExpenseService(expenseRepository);
IFinancialSummaryService summaryService = new FinancialSummaryService(incomeRepository, expenseRepository);


ConsoleUi ui = new ConsoleUi(accountService, incomeService, expenseService, summaryService);
ui.Run();
