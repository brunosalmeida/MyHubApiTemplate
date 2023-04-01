using Polly.CircuitBreaker;
using Polly.Retry;

namespace Data.Resilience;

public class IRepositoryResilience
{
    AsyncRetryPolicy _retryPolicy { get; }
    AsyncCircuitBreakerPolicy _circuitBreakerPolicy { get; }
}