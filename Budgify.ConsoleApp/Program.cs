using Budgify.Application.Interfaces;
using Budgify.Application.Services;
using Budgify.ConsoleApp;
using Budgify.Infrastructure.Memory;
using Budgify.Infrastructure.Repositories;

IAccountRepository accountRepository = new InMemoryAccountRepository();
IIncomeRepository incomeRepository = new InMemoryIncomeRepository();

IAccountService accountService = new AccountService(accountRepository);
IIncomeService incomeService = new IncomeService(incomeRepository);


ConsoleUi ui = new ConsoleUi(accountService, incomeService);
ui.Run();
