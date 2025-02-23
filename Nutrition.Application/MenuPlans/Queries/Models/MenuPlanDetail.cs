namespace Nutrition.Application.MenuPlans.Queries.Models;

public class MenuPlanDetail
{
    public Guid Id { get; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool HasSnacking { get; set; }

    private MenuPlanDetail()
    {
        Id = Guid.Empty;
    }
    private MenuPlanDetail(Guid id, DateOnly startDate, DateOnly endDate, bool hasSnacking)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
        HasSnacking = hasSnacking;
    }

    public static MenuPlanDetail CreateNew(Guid id, DateOnly startDate, DateOnly endDate, bool hasSnacking) => new(id, startDate, endDate, hasSnacking);
}
