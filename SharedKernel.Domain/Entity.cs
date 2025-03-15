using System.Text.Json.Serialization;

namespace SharedKernel.Domain;

public abstract class Entity
{
    [JsonIgnore]
    private readonly List<IDomainEvent> _uncommittedEvents = [];
    public IEnumerable<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents.AsReadOnly();
    public long Version { get; protected set; }
    
    public void ClearUncommittedEvents() => _uncommittedEvents.Clear();
    protected void AddUncommittedEvent(IDomainEvent domainEvent) => _uncommittedEvents.Add(domainEvent);
    protected static void CheckRule(IValidationRule rule)
    {
        if (rule.IsBroken()) throw new ValidationRuleException(rule.Message);
    }
    
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new BusinessRuleException(rule.Message);
    }
}
