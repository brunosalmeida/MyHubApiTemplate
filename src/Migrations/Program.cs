using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Migrations;

Console.WriteLine("Starting boostrap");
var host = CreateHostBuilder();
host.Build().Run();

Console.ReadKey();
Console.WriteLine("Finishing boostrap");

static IHostBuilder CreateHostBuilder(string[] args = null)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetBasePath(Directory.GetCurrentDirectory());
            config.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true);
            config.AddEnvironmentVariables();
        })
        .ConfigureServices((hostContext, services) =>
        {
            var postgres = hostContext.Configuration.GetSection("PostgresSQL").Get<PostgresSettings>();
            services.AddTransient<IPostgresService>((sp) =>
            {
                var logger = sp.GetService<ILogger<IPostgresService>>();
                return new PostgresService(logger, postgres);
            });

            services.AddTransient<IFluentMigrationService, FluentMigrationService>();
            
            var conn = $"server={postgres.Server};Port={postgres.Port};Database={postgres.Database};User Id={postgres.User};Password={postgres.Password};";

            services.AddFluentMigratorCore()
                .ConfigureRunner(runner =>
                    runner.AddPostgres11_0()
                        .WithGlobalConnectionString(conn)
                        .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(log => log.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
            
            services.AddHostedService<Init>();
        })
        .ConfigureLogging((hostingContext, logging) =>
        {
            logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
            logging.AddConsole();
        });
}