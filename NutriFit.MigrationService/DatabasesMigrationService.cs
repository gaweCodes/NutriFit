using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Nutrition.Infrastructure.Read.Database;

namespace NutriFit.MigrationService;

public class DatabasesMigrationService(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource _activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = _activitySource.StartActivity("Migrating databases", ActivityKind.Client);
        try
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var readDbContext = scope.ServiceProvider.GetRequiredService<NutritionReadDbContext>();
                await readDbContext.Database.MigrateAsync(cancellationToken);
                var eventsDbContext = scope.ServiceProvider.GetRequiredService<NutritionEventsDbContext>();
                await eventsDbContext.Database.MigrateAsync();
            }

        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}