using Microsoft.Extensions.Logging;
using Migrations;
using Npgsql;

public class PostgresService : IPostgresService
{
    private readonly ILogger _logger;
    private readonly PostgresSettings _settings;
    private readonly NpgsqlConnection connection;
    private NpgsqlConnection _dbConnection;

    public PostgresService(ILogger logger, PostgresSettings settings)
    {
        _logger = logger;
        connection = new NpgsqlConnection();
        connection.Open();
    }

    public Task ExecuteAsync()
    {
        _logger.LogInformation($"{nameof(PostgresService)} Starting... ");

        _logger.LogInformation($"{nameof(PostgresService)} Finished successfully ");

        return Task.CompletedTask;
    }
}