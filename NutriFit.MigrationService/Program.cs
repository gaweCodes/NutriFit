using NutriFit.Core.Databases;
using NutriFit.MigrationService;
using NutriFit.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<DatabasesMigrationService>();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(DatabasesMigrationService.ActivitySourceName));

builder.AddNpgsqlDbContext<NutriFitReadDbContext>("nutrifit-read");
builder.AddNpgsqlDbContext<NutriFitWriteDbContext>("nutrifit-write");

var host = builder.Build();
host.Run();
