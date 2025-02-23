namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlansOverview;

public class MenuPlanOverviewDto(Guid id, DateOnly startDate, DateOnly endDate, bool hasSnacking)
{
    public Guid Id { get; } = id;
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
    public bool HasSnacking { get; } = hasSnacking;
}