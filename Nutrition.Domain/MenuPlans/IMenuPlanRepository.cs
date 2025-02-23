namespace Nutrition.Domain.MenuPlans;

public interface IMenuPlanRepository
{
    Task AddAsync(MenuPlan menuPlan, CancellationToken cancellationToken);
    Task<MenuPlan> GetByIdAsync(MenuPlanId id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}