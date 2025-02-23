using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Events;

public class MenuPlanDeletedDomainEvent(Guid id) : DomainEventBase
{
    public Guid MenuPlanId { get; } = id;
}