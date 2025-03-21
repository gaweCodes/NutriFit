namespace SharedKernel.Domain;

public abstract class Entity<TKey>
    where TKey : struct, IEntityKey
{
    public TKey Id { get; protected set; }
}

public abstract class Entity<TState, TKey> : Entity<TKey>, IEventRegistryAccessor
    where TState : AggregateState<TKey>
    where TKey : struct, IEntityKey
{
    public new TKey Id => State.Id;
    protected TState State { get; set; } = null!;
    IEventRegistry IEventRegistryAccessor.GetEventRegistry() => State;
    protected void RaiseEvent(IDomainEvent domainEvent) => State.ApplyAndRegister(domainEvent);
    protected static void CheckRule(IValidationRule rule)
    {
        if (rule.IsBroken()) throw new ValidationRuleException(rule.Message);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new BusinessRuleException(rule.Message);
    }
}
