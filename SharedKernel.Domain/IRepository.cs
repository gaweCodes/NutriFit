﻿namespace SharedKernel.Domain;

public interface IRepository<TAggregate, TKey>
    where TAggregate : Entity<TKey>, IAggregateRoot
    where TKey : struct, IEntityKey
{
    Task StoreAsync(TAggregate aggregate, CancellationToken cancellationToken);
    Task<TAggregate> GetByIdAsync(TKey aggregateId, CancellationToken cancellationToken);
}
