using Budgify.Application.Interfaces;
using Budgify.Application.Services;
using Budgify.ConsoleApp;
using Budgify.ConsoleApp.Screens;
using Budgify.Infrastructure.Memory;
using Budgify.Infrastructure.Repositories;

IAccountRepository accountRepository = new InMemoryAccountRepository();
IIncomeRepository incomeRepository = new InMemoryIncomeRepository();
IExpenseRepository expenseRepository = new InMemoryExpenseRepository();
ICreditCardRepository cardRepository = new InMemoryCreditCardRepository();

IAccountService accountService = new AccountService(accountRepository);
IIncomeService incomeService = new IncomeService(incomeRepository, accountRepository);
IExpenseService expenseService = new ExpenseService(expenseRepository, accountRepository, cardRepository);
IFinancialSummaryService summaryService = new FinancialSummaryService(incomeRepository, expenseRepository);
ICreditCardService cardService = new CreditCardService(cardRepository, accountRepository, expenseRepository);

MainMenuScreen main = new MainMenuScreen(accountService, incomeService, expenseService, summaryService, cardService);
main.Run();
