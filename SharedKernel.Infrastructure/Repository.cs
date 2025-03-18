using EventStore.Client;
using MediatR;
using SharedKernel.Domain;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace SharedKernel.Infrastructure;

public class Repository<TAggregate, TKey>(EventStoreClient client, IMediator mediator) : IRepository<TAggregate, TKey>
        where TAggregate : Entity<TKey>, IAggregateRoot
    where TKey : struct, IEntityKeyValue
{
    public async Task<TAggregate> GetSpecificAsync(TKey aggregateId, CancellationToken cancellationToken)
    {
        var aggregateType = typeof(TAggregate);
        var aggregate = (TAggregate)Activator.CreateInstance(aggregateType, true)!;
        var eventTypes = aggregateType.Assembly.GetTypes();

        var result = client.ReadStreamAsync(Direction.Forwards, aggregateId.Value.ToString(), StreamPosition.Start, cancellationToken: cancellationToken);
        await foreach (var resolvedEvent in result)
        {
            var eventTypeName = resolvedEvent.Event.EventType;
            var eventType = eventTypes.Single(t => t.Name == eventTypeName);
            var specificEvent = JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, eventType);
            
            var test = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());

            var methods = aggregateType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Where(x => x.Name == "Apply").ToList();
            var applyMethod = methods.SingleOrDefault(x => x.GetParameters()[0].ParameterType.Name == resolvedEvent.Event.EventType);
            applyMethod?.Invoke(aggregate, [specificEvent]);
        }

        return aggregate;
    }


    public async Task StoreAsync(TAggregate aggregate, CancellationToken cancellationToken)
    {
        foreach (var domainEvent in aggregate.GetUncommittedEvents())
        {
            var domainEventAsBytes = JsonSerializer.SerializeToUtf8Bytes(domainEvent, domainEvent.GetType());
            var eventData = new EventData(Uuid.NewUuid(),    domainEvent.GetType().Name, domainEventAsBytes);

            await client.AppendToStreamAsync(aggregate.Id.Value.ToString(), StreamState.Any, [eventData], cancellationToken: cancellationToken);
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
