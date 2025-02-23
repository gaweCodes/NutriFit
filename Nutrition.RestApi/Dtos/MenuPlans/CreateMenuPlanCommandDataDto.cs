namespace Nutrition.RestApi.Dtos.MenuPlans;

public class CreateMenuPlanCommandDataDto
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool HasSnacking { get; set; }
}
