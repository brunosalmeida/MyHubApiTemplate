using Microsoft.Extensions.Hosting;
using Serilog;
namespace Migrations;

public class Init : IHostedService
{
    private readonly ILogger _logger;
    private readonly IPostgresService _postgresService;
    private readonly IFluentMigrationService _fluentMigrationService;

    public Init(ILogger logger, IPostgresService postgresService, 
        IFluentMigrationService fluentMigrationService)
    {
        _logger = logger;
        _postgresService = postgresService;
        _fluentMigrationService = fluentMigrationService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.Information("Starting boostrap");
        await _postgresService.ExecuteAsync();
        _fluentMigrationService.Run();
        
        await StartAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.Information("Shutting down boostrap");
    }
}