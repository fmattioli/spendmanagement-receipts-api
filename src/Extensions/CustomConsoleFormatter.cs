using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using System.Text.Json;

namespace SpendManagement.Receipts.Api.Extensions
{
    public class CustomConsoleFormatter : ConsoleFormatter
    {
        public CustomConsoleFormatter() : base("custom") { }

        public override void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
        {
            var logLevel = logEntry.LogLevel;
            var category = logEntry.Category;
            var eventId = logEntry.EventId.Id;
            var message = logEntry.Formatter(logEntry.State, logEntry.Exception);

            if (!string.IsNullOrEmpty(message))
            {
                var logRecord = new
                {
                    Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Level = logLevel.ToString(),
                    Category = category,
                    EventId = eventId,
                    Message = message,
                    Exception = logEntry.Exception?.ToString()
                };

                textWriter.WriteLine(JsonSerializer.Serialize(logRecord));
            }
        }
    }
}
