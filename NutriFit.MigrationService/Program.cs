using Microsoft.EntityFrameworkCore;
using NutriFit.MigrationService;
using NutriFit.ServiceDefaults;
using Nutrition.Infrastructure.Read.Database;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<DatabasesMigrationService>();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(DatabasesMigrationService.ActivitySourceName));
builder.Services.AddDbContext<NutritionReadDbContext>(x =>
{
    var connectionString = builder.Configuration.GetConnectionString("nutrition-read");
    x.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly));
});
builder.Services.AddDbContext<NutritionEventsDbContext>(x =>
{
    var connectionString = builder.Configuration.GetConnectionString("nutrition-events");
    x.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly));
});

await builder.Build().RunAsync();
