using MediatR;

namespace SharedKernel.Domain;

public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
}
