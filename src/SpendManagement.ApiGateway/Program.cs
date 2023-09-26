using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SpendManagement.ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("conf/ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("conf/appsettings.json", optional: false, reloadOnChange: true);

builder.Services
    .AddHealthCheckers(builder.Configuration)
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

app.UseOcelot().Wait();

app.Run();
