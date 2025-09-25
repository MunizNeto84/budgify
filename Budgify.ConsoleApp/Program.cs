using Budgify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<BudgifyDbContext>(options => options.UseNpgsql(connectionString));
});

var host = builder.Build();

Console.WriteLine("Applicação configurada. Pressione enter para sair.");
Console.ReadLine();