namespace SharedKernel.Domain;

public interface IEventRegistry
{
    IEnumerable<IDomainEvent> GetUncommittedDomainEvents();
    void ClearUncommittedDomainEvents();
}
