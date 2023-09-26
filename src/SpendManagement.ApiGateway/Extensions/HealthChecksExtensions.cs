using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using SpendManagement.ApiGateway.Extensions;

namespace SpendManagement.ApiGateway.Extensions
{
    public static class HealthChecksExtensions
    {
        private const string UrlHealthCheck = "/health";
        public static IServiceCollection AddHealthCheckers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddUrlGroup(new Uri(configuration["Settings:SpendManagementIdentity:Url"] + UrlHealthCheck), name: "SpendManagement.Identity")
                .AddUrlGroup(new Uri(configuration["Settings:SpendManagementApi:Url"] + UrlHealthCheck), name: "SpendManagement.Api")
                .AddUrlGroup(new Uri(configuration["Settings:SpendManagementReadModel:Url"] + UrlHealthCheck), name: "SpendManagement.ReadModel")
                .AddUrlGroup(new Uri(configuration["Settings:SpendManagementDomain:Url"] + UrlHealthCheck), name: "SpendManagement.Domain")
                .AddUrlGroup(new Uri(configuration["Settings:SpendManagementEventHandler:Url"] + UrlHealthCheck), name: "SpendManagement.EventHandler");

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
