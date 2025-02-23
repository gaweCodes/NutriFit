using System.ComponentModel.DataAnnotations;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Dtos;

public class MenuPlanDto 
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool HasSnacking { get; set; }
}
