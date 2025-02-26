using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class DayPlan : Entity
{
    internal DayPlanId Id { get; private set; }
    internal DateOnly Date { get; private set; }
    
    private DayPlan() { }
    internal DayPlan(DateOnly date)
    {
        Id = new DayPlanId(Guid.NewGuid());
        Date = date;              
    }
}