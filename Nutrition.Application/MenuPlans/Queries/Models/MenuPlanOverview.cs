namespace Nutrition.Application.MenuPlans.Queries.Models;

public class MenuPlanOverview
{
    public Guid Id { get; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool HasSnacking { get; set; }

    private MenuPlanOverview()
    {
        Id = Guid.Empty;
    }
    private MenuPlanOverview(Guid id, DateOnly startDate, DateOnly endDate, bool hasSnacking)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        HasSnacking = hasSnacking;
    }

    public static MenuPlanOverview CreateNew(Guid id, DateOnly startDate, DateOnly endDate, bool hasSnacking) => new(id, startDate, endDate, hasSnacking);
}
