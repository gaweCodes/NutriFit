using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Domain;

namespace SharedKernel.Infrastructure;

public class EventPublisher(IServiceProvider serviceProvider) : SaveChangesInterceptor
{
    private static List<Entity>? savedEntities = [];
    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        savedEntities = eventData.Context?.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added)
            .Select(e => e.Entity)
            .Cast<Entity>()
            .ToList();
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public async override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        foreach (var entity in savedEntities ?? [])
        {
            var domainEvents = entity.DomainEvents.ToList();
            foreach (var domainEvent in domainEvents)
            {
                var mediator = serviceProvider.GetRequiredService<IMediator>();
                await mediator.Publish(domainEvent, cancellationToken);
            }
            
            entity.ClearDomainEvents();
        }
        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }
}
