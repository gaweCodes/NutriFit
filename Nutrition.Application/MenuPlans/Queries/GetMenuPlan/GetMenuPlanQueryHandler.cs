using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlan;

internal class GetMenuPlanQueryHandler(IReadMenuPlanRepository readMenuPlanRepository) : IQueryHandler<GetMenuPlanQuery, MenuPlanDto?>
{
    public async Task<MenuPlanDto?> Handle(GetMenuPlanQuery query, CancellationToken cancellationToken)
    {
        var menuPlan = await readMenuPlanRepository.GetMenuPlanDetailAsync(query.Id, cancellationToken);
        return menuPlan is not null ? new MenuPlanDto(menuPlan.StartDate, menuPlan.EndDate) : null;
    }
}