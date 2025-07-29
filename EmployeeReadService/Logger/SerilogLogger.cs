using EmployeeReadService.Interfaces;
using Serilog;

namespace EmployeeReadService.Logger
{
    public class SerilogLogger<T> : IAppLogger<T>
    {
        private readonly Serilog.ILogger _logger;

        public SerilogLogger()
        {
            _logger = Log.ForContext<T>();
        }

        public void LogInformation(string message) => _logger.Information(message);
        public void LogWarning(string message) => _logger.Warning(message);
        public void LogError(string message, Exception ex = null) =>
            _logger.Error(ex, message);
    }
}
