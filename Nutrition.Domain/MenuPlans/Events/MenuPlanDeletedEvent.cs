using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Events;

public class MenuPlanDeletedDomainEvent(Guid id) : IDomainEvent
{
    public Guid MenuPlanId { get; } = id;
}