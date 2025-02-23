using Nutrition.Application.MenuPlans.Queries.Models;

namespace Nutrition.Application.MenuPlans.Queries;

public interface IReadMenuPlanRepository
{
    public Task<MenuPlanDetail?> GetMenuPlanDetailAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<MenuPlanOverview>> GetMenuPlansOverviewAsync(CancellationToken cancellationToken);
}
