using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Services;

public class MenuPlanOverlapChecker(IMenuPlanRepository repository)
{
    public async Task CheckDoesNotOverlapWithExistingPlans(MenuPlan plan) 
    {
        var menuPlans = await repository.GetAllAsync();
        if (menuPlans.Any(x => x.IsOverlapingWith(plan)))
            throw new ValidationException("Der soeben bearbeitete Menuplan überschneidet sich mit einem anderen.");
    }
}
