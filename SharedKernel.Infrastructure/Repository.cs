using Marten;
using MediatR;
using SharedKernel.Domain;

namespace SharedKernel.Infrastructure;

public class Repository<TAggregate, TStreamId>(IDocumentStore store, IMediator mediator) : IRepository<TAggregate, TStreamId> 
    where TAggregate : Entity, IAggregateRoot
    where TStreamId : struct, IAggregateId
{
    public async Task<TAggregate> GetSpecificAsync(TStreamId streamId, CancellationToken cancellationToken)
    {
        await using var session = await store.LightweightSerializableSessionAsync(cancellationToken);
        var aggregate = await session.Events.AggregateStreamAsync<TAggregate>(streamId.Value, token: cancellationToken);
        return aggregate ?? throw new EntityNotFoundException(typeof(TAggregate).Name, streamId.Value);
    }

    public async Task StoreAsync(TAggregate aggregate, TStreamId streamId, CancellationToken cancellationToken)
    {
        await using var session = await store.LightweightSerializableSessionAsync(cancellationToken);
        var domainEvents = aggregate.GetUncommittedEvents();
        session.Events.Append(streamId.Value, aggregate.Version, domainEvents);
        await session.SaveChangesAsync(cancellationToken);
        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);
        aggregate.ClearUncommittedEvents();
    }
}