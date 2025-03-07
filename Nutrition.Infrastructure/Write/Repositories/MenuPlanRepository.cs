using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.MenuPlans;
using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;
using Nutrition.Infrastructure.Write.Database;
using SharedKernel.Domain;

namespace Nutrition.Infrastructure.Write.Repositories;

public class MenuPlanRepository(NutritionWriteDbContext dbContext) : IMenuPlanRepository
{
    public async Task AddAsync(MenuPlan menuPlan, CancellationToken cancellationToken) => 
        await dbContext.AddAsync(menuPlan, cancellationToken);

    public async Task<MenuPlan> GetByIdAsync(MenuPlanId id, CancellationToken cancellationToken)
    {
        var menuPlan = await dbContext.Set<MenuPlan>()
            .Include(mp => mp.Days)
            .ThenInclude(x => x.MealSlots)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        return menuPlan is null ? throw new EntityNotFoundException(nameof(MenuPlan), id.Value) : menuPlan;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
}
