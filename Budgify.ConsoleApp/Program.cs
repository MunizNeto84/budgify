using Budgify.ConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Budgify.ConsoleApp.Extensions;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory);
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    });

builder.ConfigureServices((hostContext, services) =>
{
    services.AddInfrastructure(hostContext.Configuration);
    services.AddApplication();

    services.AddTransient<ConsoleUi>();
});

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var app = services.GetRequiredService<ConsoleUi>();

    app.Run();
}