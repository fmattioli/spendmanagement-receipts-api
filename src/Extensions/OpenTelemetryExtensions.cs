using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SpendManagement.Receipts.Api.Models;

namespace SpendManagement.Receipts.Api.Extensions
{
    public static class OpenTelemetryExtensions
    {
        public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, MltConfigsSettings mltSettings)
        {
            var serviceName = "Receipts.Api";
            var otelExporterEndpoint = mltSettings.GrafanaLokiUrl;

            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName))
                .UseOtlpExporter(OtlpExportProtocol.Grpc, new Uri(otelExporterEndpoint!))
                .WithTracing(builder =>
                {
                    builder
                        .AddSource(serviceName)
                        .AddAspNetCoreInstrumentation();
                })
                .WithMetrics(builder =>
                {
                    builder
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddProcessInstrumentation();
                });

            services.AddSingleton(TracerProvider.Default.GetTracer(serviceName));

            return services;
        }
    }
}
