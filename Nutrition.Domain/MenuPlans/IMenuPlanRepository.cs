namespace Nutrition.Domain.MenuPlans;

public interface IMenuPlanRepository
{
    Task AddAsync(MenuPlan menuPlan, CancellationToken cancellationToken);
    Task<MenuPlan> GetByIdAsync(MenuPlanId id, CancellationToken cancellationToken);
    Task<List<MenuPlan>> GetAllAsync();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}