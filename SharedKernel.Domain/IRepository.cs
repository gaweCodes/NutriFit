namespace SharedKernel.Domain;

public interface IRepository<TAggregate, TKey> 
    where  TAggregate : Entity<TKey>, IAggregateRoot
    where TKey : struct, IEntityKeyValue
{
    Task StoreAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> GetSpecificAsync(TKey aggregateId, CancellationToken cancellationToken);
}
