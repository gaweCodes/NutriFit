using Nutrition.Application.MenuPlans.Queries.Models;

namespace Nutrition.Application.MenuPlans.Queries;

public interface IReadMenuPlanRepository
{
    Task<MenuPlanDetail> GetMenuPlanDetailAsync(Guid id, CancellationToken cancellationToken);
    Task<List<MenuPlanOverview>> GetMenuPlanOverviewAsync(CancellationToken cancellationToken);
}
