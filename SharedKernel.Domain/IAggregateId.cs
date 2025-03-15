namespace SharedKernel.Domain;

public interface IAggregateId
{
    public Guid Value { get; }
}
