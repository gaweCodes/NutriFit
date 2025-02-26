namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlan;

public class MenuPlanDto(DateOnly startDate, DateOnly endDate)
{
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
}