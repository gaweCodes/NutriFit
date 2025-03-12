using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlanOverview;

internal class GetMenuPlanOverviewQueryHandler(IReadMenuPlanRepository readMenuPlanRepository) : IQueryHandler<GetMenuPlanOverviewQuery, List<MenuPlanOverviewDto>>
{
    public async Task<List<MenuPlanOverviewDto>> Handle(GetMenuPlanOverviewQuery query, CancellationToken cancellationToken)
    {
        var menuPlanOverview = await readMenuPlanRepository.GetMenuPlanOverviewAsync(cancellationToken);
        return [.. menuPlanOverview.Select(x => new MenuPlanOverviewDto(x.Id, x.StartDate, x.EndDate))];
    }
}