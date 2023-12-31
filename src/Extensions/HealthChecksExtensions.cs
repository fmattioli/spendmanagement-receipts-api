using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using SpendManagement.ApiGateway.Extensions;
using SpendManagement.ApiGateway.Models;

namespace SpendManagement.ApiGateway.Extensions
{
    public static class HealthChecksExtensions
    {
        private const string UrlHealthCheck = "/health";

        public static IServiceCollection AddHealthCheckers(this IServiceCollection services, Settings? applicationSettings)
        {
            services.AddHealthChecks()
                .AddUrlGroup(new Uri(applicationSettings?.SpendManagementIdentity?.Url + UrlHealthCheck), name: "SpendManagement.Identity")
                .AddUrlGroup(new Uri(applicationSettings?.SpendManagementApi?.Url + UrlHealthCheck), name: "SpendManagement.Api")
                .AddUrlGroup(new Uri(applicationSettings?.SpendManagementReadModel?.Url + UrlHealthCheck), name: "SpendManagement.ReadModel")
                .AddUrlGroup(new Uri(applicationSettings?.SpendManagementDomain?.Url + UrlHealthCheck), name: "SpendManagement.Domain")
                .AddUrlGroup(new Uri(applicationSettings?.SpendManagementEventHandler?.Url + UrlHealthCheck), name: "SpendManagement.EventHandler");

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
