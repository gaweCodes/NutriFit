using EventStore.Client;
using MediatR;
using SharedKernel.Domain;
using System.Reflection;
using System.Text.Json;

namespace SharedKernel.Infrastructure;

public class Repository<TAggregate, TKey>(EventStoreClient client, IMediator mediator) : IRepository<TAggregate, TKey>
    where TAggregate : Entity<TKey>, IAggregateRoot
    where TKey : struct, IEntityKeyValue
{
    private static readonly Dictionary<string, Type> EventTypeCache = typeof(TAggregate).Assembly.GetTypes()
        .Where(t => typeof(IDomainEvent).IsAssignableFrom(t))
        .ToDictionary(t => t.Name, t => t);

    private static readonly Dictionary<Type, MethodInfo> ApplyMethodCache = typeof(TAggregate)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
        .Where(m => m.Name == "Apply" && m.GetParameters().Length == 1)
        .ToDictionary(m => m.GetParameters()[0].ParameterType, m => m);

    public async Task<TAggregate> GetSpecificAsync(TKey aggregateId, CancellationToken cancellationToken)
    {
        var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true)!;

        var result = client.ReadStreamAsync(Direction.Forwards, aggregateId.Value.ToString(), StreamPosition.Start, cancellationToken: cancellationToken);
        await foreach (var resolvedEvent in result)
        {
            if (!EventTypeCache.TryGetValue(resolvedEvent.Event.EventType, out var eventType))
                throw new InvalidOperationException($"Unknown event type: {resolvedEvent.Event.EventType}");

            var specificEvent = JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, eventType) ?? throw new InvalidOperationException($"Failed to deserialize event of type {eventType.Name}");
            if (ApplyMethodCache.TryGetValue(eventType, out var applyMethod) && applyMethod != null)
                applyMethod.Invoke(aggregate, [specificEvent]);
        }

        return aggregate;
    }

    public async Task StoreAsync(TAggregate aggregate, CancellationToken cancellationToken)
    {
        var uncommittedEvents = aggregate.GetUncommittedEvents().ToArray();
        var streamName = aggregate.Id.Value.ToString();

        foreach (var domainEvent in uncommittedEvents)
        {
            var domainEventAsByteArray = JsonSerializer.SerializeToUtf8Bytes(domainEvent, domainEvent.GetType());
            var eventToAppend = new EventData(Uuid.NewUuid(), domainEvent.GetType().Name, domainEventAsByteArray);
            await client.AppendToStreamAsync(streamName, StreamState.Any, [eventToAppend], cancellationToken: cancellationToken);
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
