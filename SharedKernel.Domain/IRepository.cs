namespace SharedKernel.Domain;

public interface IRepository<TAggregate, TAggregateId> 
    where  TAggregate : Entity, IAggregateRoot
    where TAggregateId : struct, IAggregateId
{
    Task StoreAsync(TAggregate aggregate, TAggregateId  aggregateId, CancellationToken cancellationToken);
    Task<TAggregate> GetSpecificAsync(TAggregateId aggregateId, CancellationToken cancellationToken);
}
