using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NutriFit.ApiService.Databases;
using OpenTelemetry.Trace;

namespace NutriFit.MigrationsService;

public class Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await MigrateReadDatabaseAsync(stoppingToken);
        await MigrateWriteDatabaseAsync(stoppingToken);
    }

    private async Task MigrateReadDatabaseAsync(CancellationToken stoppingToken)
    {
        const string migrateReadDatabaseActivity = "migrating database read";
        using var activity = ActivitySource.StartActivity(migrateReadDatabaseActivity, ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NutriFitReadDbContext>();
            var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                if (!await dbCreator.ExistsAsync(stoppingToken))
                    await dbCreator.CreateAsync(stoppingToken);
                
                await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
                await dbContext.Database.MigrateAsync(stoppingToken);
                await transaction.CommitAsync(stoppingToken);
            });


        }
        catch (Exception e)
        {
            activity?.RecordException(e);
            throw;
        }
        
        hostApplicationLifetime.StopApplication();
    }

    private async Task MigrateWriteDatabaseAsync(CancellationToken stoppingToken)
    {
        const string migrateWriteDatabaseActivity = "migrating database write";
        using var activity = ActivitySource.StartActivity(migrateWriteDatabaseActivity, ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<NutriFitWriteDbContext>();
            var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
            var strategy = dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                if (!await dbCreator.ExistsAsync(stoppingToken))
                    await dbCreator.CreateAsync(stoppingToken);

                await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
                await dbContext.Database.MigrateAsync(stoppingToken);
                await transaction.CommitAsync(stoppingToken);
            });


        }
        catch (Exception e)
        {
            activity?.RecordException(e);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}
