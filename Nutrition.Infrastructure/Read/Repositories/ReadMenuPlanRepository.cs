using Microsoft.EntityFrameworkCore;
using Nutrition.Application.MenuPlans.Queries;
using Nutrition.Application.MenuPlans.Queries.Models;
using Nutrition.Infrastructure.Read.Database;
using SharedKernel.Infrastructure;

namespace Nutrition.Infrastructure.Read.Repositories;

internal class ReadMenuPlanRepository(NutritionReadDbContext dbContext) : IReadMenuPlanRepository
{
    public async Task<MenuPlanDetail> GetMenuPlanDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var menuPlanDetail = await dbContext.Set<MenuPlanDetail>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        return menuPlanDetail is null ? throw new EntityNotFoundException(nameof(MenuPlanDetail), id) : menuPlanDetail;
    }

    public async Task<List<MenuPlanOverview>> GetMenuPlanOverviewAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<MenuPlanOverview>().ToListAsync(cancellationToken);
}