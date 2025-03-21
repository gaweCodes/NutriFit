using System.Reflection;
using System.Text.Json.Serialization;

namespace SharedKernel.Domain;

public abstract class AggregateState<TKey> : IEventRegistry
    where TKey : struct, IEntityKey
{
    [JsonIgnore]
    private readonly List<IDomainEvent> _uncommittedDomainEvents = [];
    public TKey Id { get; protected set; }
    public IEnumerable<IDomainEvent> GetUncommittedDomainEvents() => _uncommittedDomainEvents.AsReadOnly();
    public void ClearUncommittedDomainEvents() => _uncommittedDomainEvents.Clear();

    internal void ApplyAndRegister(IDomainEvent domainEvent)
    {
        _uncommittedDomainEvents.Add(domainEvent);
        var type = this.GetType();
        var eventType = domainEvent.GetType();
        var applyMethod = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Single(x => x.Name == "Apply" && x.GetParameters()[0].ParameterType.Name == eventType.Name);
        applyMethod.Invoke(this, [domainEvent]);
    }
}
