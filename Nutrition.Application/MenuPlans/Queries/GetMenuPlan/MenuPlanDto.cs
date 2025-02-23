namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlan;

public class MenuPlanDto(DateOnly startDate, DateOnly endDate, bool hasSnacking)
{
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
    public bool HasSnacking { get; } = hasSnacking;
}