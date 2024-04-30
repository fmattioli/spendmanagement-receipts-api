using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SpendManagement.Receipts.Api.Extensions;
using SpendManagement.Receipts.Api.Models;

var builder = WebApplication.CreateBuilder(args);

var enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

builder.Configuration
    .AddJsonFile("conf/appsettings.json", false, reloadOnChange: true)
    .AddJsonFile($"conf/appsettings.{enviroment}.json", true, reloadOnChange: true)
    .AddJsonFile("conf/ocelot.json", true, reloadOnChange: true)
    .AddJsonFile($"conf/ocelot.{enviroment}.json", true, reloadOnChange: true)
    .AddEnvironmentVariables();

var applicationSettings = builder.Configuration.GetSection("Settings").Get<Settings>();

builder.Services
    .AddHealthCheckers(applicationSettings)
    .AddOcelot(builder.Configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseHealthChecks();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.UseSwaggerForOcelotUI(options =>
{
    options.DownstreamSwaggerEndPointBasePath = "/swagger/docs";
    options.PathToSwaggerGenerator = "/swagger/docs";
});

app.MapGet("/felipe", () => enviroment);
await app.UseOcelot();
app.Run();
