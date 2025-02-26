using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Events;

public class MenuPlanUpdatedDomainEvent(Guid id, DateOnly startDate, DateOnly endDate) : DomainEventBase
{
    public Guid MenuPlanId { get; } = id;
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
}