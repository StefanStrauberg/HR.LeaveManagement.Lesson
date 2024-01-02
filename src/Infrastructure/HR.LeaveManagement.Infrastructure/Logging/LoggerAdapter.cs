using HR.LeaveManagement.Application.Contracts.Logging;
using Microsoft.Extensions.Logging;

namespace HR.LeaveManagement.Infrastructure.Logging;

internal class LoggerAdapter<T>(ILoggerFactory loggerFactory) : IAppLogger<T>
{
    readonly ILogger<T> _logger = loggerFactory.CreateLogger<T>()
        ?? throw new ArgumentNullException(nameof(loggerFactory));

    public void LogInformation(string message,
                               params object[] args)
        => _logger.LogInformation(message,
                                  args);

    public void LogWarning(string message,
                           params object[] args)
        => _logger.LogError(message,
                            args);
}