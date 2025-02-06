using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NutriFit.MigrationService;
using NutriFit.ServiceDefaults;
using Nutrition.Infrastructure.Databases;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<DatabasesMigrationService>();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(DatabasesMigrationService.ActivitySourceName));

builder.Services.AddDbContext<NutritionWriteDbContext>(x =>
{
    var connectionString = builder.Configuration.GetConnectionString("nutrition-write");
    x.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly));
});
builder.Services.AddDbContext<NutritionReadDbContext>(x =>
{
    var connectionString = builder.Configuration.GetConnectionString("nutrition-read");
    x.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(Program).Assembly));
});

var host = builder.Build();
host.Run();
