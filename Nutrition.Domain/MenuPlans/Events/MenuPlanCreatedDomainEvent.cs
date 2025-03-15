using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Events;

public class MenuPlanCreatedDomainEvent(Guid id, DateOnly startDate, DateOnly endDate) : IDomainEvent
{
    public Guid MenuPlanId { get; } = id;
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
}