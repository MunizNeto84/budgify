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
    services.AddScoped<IIncomeService, IncomeService>();
    services.AddScoped<IIncomeRepository, IncomeRepository>();
});

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var accountService = services.GetRequiredService<IAccountService>();
    var incomeService = services.GetRequiredService<IIncomeService>();

    Console.WriteLine("--- Cadastro de Nova Conta ---");

    Console.Write("Nome do Banco: ");
    var bankName = Console.ReadLine();

    Console.Write("Saldo Inicial: ");
    var initialBalance = decimal.Parse(Console.ReadLine());

    Console.Write("Possui Cartão de Crédito (s/n): ");
    var hasCreditCard = Console.ReadLine().ToLower() == "s";

    accountService.CreateAccount(bankName, initialBalance, hasCreditCard);

    Console.WriteLine("\nConta criada com sucesso!");

    Console.WriteLine("\n--- Cadastro de Nova Receita ---");

    var date = DateTime.UtcNow;
    Console.WriteLine($"Data: {date.ToShortDateString()}");

    Console.WriteLine("Categoria (Ex: Salário, Vendas): ");
    var category = Console.ReadLine()!;

    Console.WriteLine("Tipo (Ex: Fixo, Variável): ");
    var incomeType = Console.ReadLine()!;

    Console.WriteLine("Valor: ");
    var amount = decimal.Parse(Console.ReadLine()!);

    incomeService.CreateIncome(date, category, incomeType, amount);

    Console.WriteLine("\nReceita criada com sucesso!");
}