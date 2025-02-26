namespace Nutrition.Application.MenuPlans.Queries.Models;

public class MenuPlanDetail
{
    public Guid Id { get; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    private MenuPlanDetail() { }

    public MenuPlanDetail(Guid id, DateOnly startDate, DateOnly endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }
}
