using Nutrition.Domain.MenuPlans.Checkers;
using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Infrastructure.Write.Database;

namespace Nutrition.Infrastructure.Write.Checkers;

internal class UniqueMenuPlanDateRangeChecker(NutritionWriteDbContext dbContext) : IUniqueMenuPlanDateRangeChecker
{
    public bool Check(MenuPlan menuPlan)
    {
        return !dbContext.Set<MenuPlan>().Any(existingPlan =>
            existingPlan.Id != menuPlan.Id && 
            existingPlan.StartDate <= menuPlan.EndDate &&
            menuPlan.StartDate <= existingPlan.EndDate
        );
    }
}
