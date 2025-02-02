using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using NutriFit.Core.Databases;

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
            using var scope = serviceProvider.CreateScope();
            var writeDbContext = scope.ServiceProvider.GetRequiredService<NutriFitWriteDbContext>();
            await writeDbContext.Database.MigrateAsync();
            var readDbContext = scope.ServiceProvider.GetRequiredService<NutriFitReadDbContext>();
            await readDbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}