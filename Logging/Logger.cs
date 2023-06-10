using Serilog;
using Serilog.Events;
using Serilog.AspNetCore;
using Serilog.Context;
using Serilog.Sinks.SystemConsole.Themes;
using Microsoft.AspNetCore.Http;

namespace CompanyManager.Logging
{
    public static class Logger
    {
        private static Serilog.ILogger _logger;
        private static Serilog.ILogger _networkLogger;

    public static void Configure(ClientIpEnricher clientIpEnricher)
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.With(clientIpEnricher)
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{Exception}{ClientIP}{NewLine}",
                theme: SystemConsoleTheme.Literate)
            .WriteTo.File("CompanyManager.log", 
                            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{Exception} {ClientIP}{NewLine}",
                            rollingInterval: RollingInterval.Day)
            .CreateLogger();

            _networkLogger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.With(clientIpEnricher)
                .WriteTo.File("Network.log",
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{Exception}{NewLine}",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();
    }


        public static void LogInformation(string message)
        {
            _logger.Information(message);
        }

        public static void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public static void LogError(string message)
        {
            _logger.Error(message);
        }

        public static void LogException(Exception exception)
        {
            _logger.Error(exception, "An error occurred.");
        }
        public static void LogInformationForNetworkLog(string message)
        {
            _networkLogger.Information(message);
        }
    }
}
