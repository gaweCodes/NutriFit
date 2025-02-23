using Microsoft.EntityFrameworkCore;
using Nutrition.Application.MenuPlans.Queries;
using Nutrition.Application.MenuPlans.Queries.Models;
using Nutrition.Infrastructure.Read.Database;

namespace Nutrition.Infrastructure.Read.Repositories;

internal class ReadMenuPlanRepository(NutritionReadDbContext dbContext) : IReadMenuPlanRepository
{
    public async Task<MenuPlanDetail?> GetMenuPlanDetailAsync(Guid id, CancellationToken cancellationToken) =>
        await dbContext.FindAsync<MenuPlanDetail>(id, cancellationToken);
    
    public async Task<List<MenuPlanOverview>> GetMenuPlansOverviewAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<MenuPlanOverview>().ToListAsync(cancellationToken);
}