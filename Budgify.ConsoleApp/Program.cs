using Budgify.Application.Interfaces;
using Budgify.Application.Services;
using Budgify.ConsoleApp;
using Budgify.Infrastructure.Memory;

IAccountRepository repository = new InMemoryAccountRepository();
IAccountService service = new AccountService(repository);

ConsoleUi ui = new ConsoleUi(service);
ui.Run();
