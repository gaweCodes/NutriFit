using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlansOverview;

internal class GetMenuPlansOverviewQueryHandler(IReadMenuPlanRepository readMenuPlanRepository) : IQueryHandler<GetMenuPlansOverviewQuery, List<MenuPlanOverviewDto>>
{
    public async Task<List<MenuPlanOverviewDto>> Handle(GetMenuPlansOverviewQuery query, CancellationToken cancellationToken)
    {
        var menuPlansOverview = await readMenuPlanRepository.GetMenuPlansOverviewAsync(cancellationToken);
        return menuPlansOverview.Select(x => new MenuPlanOverviewDto(x.Id, x.StartDate, x.EndDate)).ToList();
    }
}