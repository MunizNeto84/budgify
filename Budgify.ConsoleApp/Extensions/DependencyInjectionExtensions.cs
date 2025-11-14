using Budgify.Application;
using Budgify.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Budgify.ConsoleApp.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BudgifyDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IFinancialSummaryService, FinancialSummaryService>();

        return services;
    }
}