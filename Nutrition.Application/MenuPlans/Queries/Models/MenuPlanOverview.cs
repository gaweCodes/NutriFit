namespace Nutrition.Application.MenuPlans.Queries.Models;

public class MenuPlanOverview
{
    public Guid Id { get; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    private MenuPlanOverview() { }
    
    public MenuPlanOverview(Guid id, DateOnly startDate, DateOnly endDate)
    {
        Id = id;
        StartDate = startDate;
        EndDate = endDate;
    }
}
