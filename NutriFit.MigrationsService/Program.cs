using NutriFit.ApiService.Databases;
using NutriFit.MigrationsService;
using NutriFit.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry()
    .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));
builder.AddNpgsqlDbContext<NutriFitWriteDbContext>("nutriFitWrite");
builder.AddNpgsqlDbContext<NutriFitReadDbContext>("nutriFitRead");

var host = builder.Build();
host.Run();