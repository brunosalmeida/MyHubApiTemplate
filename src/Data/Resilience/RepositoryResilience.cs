using Npgsql;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;

namespace Data.Resilience;

public class RepositoryResilience : IRepositoryResilience
{
    public AsyncRetryPolicy RetryPolicy { get; }
    public AsyncCircuitBreakerPolicy CircuitBreakerPolicy { get; }
    
    private const int RETRY_COUNT = 3;
    private const int EXCEPTIONS_ALLOWED_BEFORE_BREAKING = 3;
    private const int DURATION_OF_BREAK = 3;

    public RepositoryResilience()
    {
        RetryPolicy = Policy
            .Handle<NpgsqlException>()
            .Or<Exception>()
            .WaitAndRetryAsync(
                retryCount: RETRY_COUNT,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, timespan) =>
                {
                    Console.WriteLine($"Retry due to exception: {exception.Message}. Waiting {timespan.TotalSeconds} seconds before retrying.");
                }
            );

        CircuitBreakerPolicy = Policy
            .Handle<NpgsqlException>()
            .Or<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: EXCEPTIONS_ALLOWED_BEFORE_BREAKING,
                durationOfBreak: TimeSpan.FromSeconds(DURATION_OF_BREAK),
                onBreak: (exception, timespan) =>
                {
                    Console.WriteLine($"Circuit breaker opened due to exception: {exception.Message}. Waiting {timespan.TotalSeconds} seconds before trying again.");
                },
                onReset: () =>
                {
                    Console.WriteLine("Circuit breaker closed.");
                }
            );
    }
}