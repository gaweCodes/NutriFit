using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Events;

public class MenuPlanUpdatedDomainEvent(Guid id, DateOnly startDate, DateOnly endDate, bool hasSnacking) : DomainEventBase
{
    public Guid MenuPlanId { get; } = id;
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
    public bool HasSnacking { get; } = hasSnacking;
}