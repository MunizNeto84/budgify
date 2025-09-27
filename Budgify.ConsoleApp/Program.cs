using Budgify.Application;
using Budgify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    });

builder.ConfigureServices((hostContext, services) =>
{
    var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<BudgifyDbContext>(options =>
        options.UseNpgsql(connectionString));

    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<IAccountRepository, AccountRepository>();
});

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var accountService = services.GetRequiredService<IAccountService>();

    Console.WriteLine("--- Cadastro de Nova Conta ---");

    Console.Write("Nome do Banco: ");
    var bankName = Console.ReadLine();

    Console.Write("Saldo Inicial: ");
    var initialBalance = decimal.Parse(Console.ReadLine());

    Console.Write("Possui Cartão de Crédito (s/n): ");
    var hasCreditCard = Console.ReadLine().ToLower() == "s";

    accountService.CreateAccount(bankName, initialBalance, hasCreditCard);

    Console.WriteLine("\nConta criada com sucesso!");
}