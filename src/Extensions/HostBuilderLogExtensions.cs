using Serilog;

namespace SpendManagement.Receipts.Api.Extensions
{
    public static class HostBuilderLogExtensions
    {
        public static IHostBuilder UseSerilog(this IHostBuilder builder)
        {
            return builder.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(context.Configuration)
                    .ApplyFilters();
            });
        }

        private static void ApplyFilters(this LoggerConfiguration loggerConfiguration)
        {
            loggerConfiguration.Filter.ByExcluding(c =>
                c.Properties.Any(p => p.Value.ToString().Contains("swagger", StringComparison.OrdinalIgnoreCase))
            );
            loggerConfiguration.Filter.ByExcluding(c =>
                c.Properties.Any(p => p.Value.ToString().Contains("browserLink", StringComparison.OrdinalIgnoreCase))
            );
            loggerConfiguration.Filter.ByExcluding(c =>
                c.Properties.Any(p => p.Value.ToString().Contains("aspnetcore-browser-refresh.js", StringComparison.OrdinalIgnoreCase))
            );
        }
    }
}
