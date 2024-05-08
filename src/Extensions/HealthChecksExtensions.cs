using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using SpendManagement.Receipts.Api.Extensions;
using SpendManagement.Receipts.Api.Models;

namespace SpendManagement.Receipts.Api.Extensions
{
    public static class HealthChecksExtensions
    {
        private const string UrlHealthCheck = "/health";

        public static IServiceCollection AddHealthCheckers(this IServiceCollection services, Settings? applicationSettings)
        {
            services.AddHealthChecks()
                .AddUrlGroup(new Uri(applicationSettings?.SpendManagementIdentityApi?.Url + UrlHealthCheck), name: "SpendManagement.Identity")
                .AddUrlGroup(new Uri(applicationSettings?.ReceiptsCommandHandlerApi?.Url + UrlHealthCheck), name: "SpendManagement.Receipts.CommandHandler.Api")
                .AddUrlGroup(new Uri(applicationSettings?.ReceiptsQueryHandlerApi?.Url + UrlHealthCheck), name: "SpendManagement.Receipts.QueryHandler.Api")
                .AddUrlGroup(new Uri(applicationSettings?.ReceiptsDomainApi?.Url + UrlHealthCheck), name: "SpendManagement.Receipts.Domain.Api");

            services.AddHealthChecksUI()
                .AddInMemoryStorage();

            return services;
        }

        public static void UseHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => options.UIPath = "/monitor");
        }
    }
}
