using EventStore.Client;
using MediatR;
using SharedKernel.Domain;
using System.Reflection;
using System.Text.Json;

namespace SharedKernel.Infrastructure;

public class Repository<TAggregate, TState, TKey>(EventStoreClient client, IMediator mediator) : IRepository<TAggregate, TKey>
    where TAggregate : Entity<TState, TKey>, IAggregateRoot<TAggregate, TState, TKey>
    where TState : AggregateState<TKey>
    where TKey : struct, IEntityKey
{
    private static readonly Dictionary<string, Type> EventTypeCache = typeof(TAggregate).Assembly.GetTypes()
        .Where(t => typeof(IDomainEvent).IsAssignableFrom(t))
        .ToDictionary(t => t.Name, t => t);

    private static readonly Dictionary<Type, MethodInfo> ApplyMethodCache = typeof(TState)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
        .Where(m => m.Name == "Apply" && m.GetParameters().Length == 1)
        .ToDictionary(m => m.GetParameters()[0].ParameterType, m => m);

    public async Task<TAggregate> GetByIdAsync(TKey aggregateId, CancellationToken cancellationToken)
    {
        var aggregateState = (TState)Activator.CreateInstance(typeof(TState), true)!;
        var result = client.ReadStreamAsync(Direction.Forwards, aggregateId.Value.ToString(), StreamPosition.Start, cancellationToken: cancellationToken);
        await foreach (var loadedDomainEvent in result)
        {
            var specificEvent = JsonSerializer.Deserialize(loadedDomainEvent.Event.Data.Span, EventTypeCache[loadedDomainEvent.Event.EventType]);
            var applyMethod = ApplyMethodCache[specificEvent!.GetType()];
            applyMethod.Invoke(aggregateState, [specificEvent]);
        }

        return TAggregate.FromState(aggregateState);
    }

    public async Task StoreAsync(TAggregate aggregate, CancellationToken cancellationToken)
    {
        var eventRegistry = ((IEventRegistryAccessor)aggregate).GetEventRegistry();
        var uncommittedEvents = eventRegistry.GetUncommittedDomainEvents().ToArray();
        var streamName = aggregate.Id.Value.ToString();

        foreach (var domainEvent in uncommittedEvents)
        {
            var domainEventAsByteArray = JsonSerializer.SerializeToUtf8Bytes(domainEvent, domainEvent.GetType());
            var eventToAppend = new EventData(Uuid.NewUuid(), domainEvent.GetType().Name, domainEventAsByteArray);
            await client.AppendToStreamAsync(streamName, StreamState.Any, [eventToAppend], cancellationToken: cancellationToken);
            await mediator.Publish(domainEvent, cancellationToken);
        }
        eventRegistry.ClearUncommittedDomainEvents();
    }
}
