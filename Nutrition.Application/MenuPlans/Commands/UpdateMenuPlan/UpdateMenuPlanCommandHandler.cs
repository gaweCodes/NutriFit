using Nutrition.Domain.MenuPlans;
using Nutrition.Domain.MenuPlans.Checkers;
using Nutrition.Domain.MenuPlans.ValueObjects;
using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.UpdateMenuPlan;

internal class UpdateMenuPlanCommandHandler(IMenuPlanRepository menuPlanRepository, IUniqueMenuPlanDateRangeChecker uniqueMenuPlanDateRangeChecker) : ICommandHandler<UpdateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(UpdateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = await menuPlanRepository.GetByIdAsync(new MenuPlanId(request.Id), cancellationToken);
        menuPlan.UpdateMenuPlan(request.StartDate, request.EndDate, uniqueMenuPlanDateRangeChecker);
        await menuPlanRepository.SaveChangesAsync(cancellationToken);
        return menuPlan.Id.Value;
    }
}