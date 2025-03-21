namespace SharedKernel.Domain;

public interface IAggregateRoot;
public interface IAggregateRoot<TAggregate, TState, TKey> : IAggregateRoot
    where TAggregate : Entity<TState, TKey>, IAggregateRoot<TAggregate, TState, TKey>
    where TState : AggregateState<TKey>
    where TKey : struct, IEntityKey
{
    abstract static TAggregate FromState(TState state);
}