using Nutrition.Domain.MenuPlans;
using Nutrition.Domain.MenuPlans.Services;
using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.UpdateMenuPlan;

internal class UpdateMenuPlanCommandHandler(IMenuPlanRepository menuPlanRepository, MenuPlanOverlapChecker menuPlanOverlapChecker) : ICommandHandler<UpdateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(UpdateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = await menuPlanRepository.GetByIdAsync(new MenuPlanId(request.Id), cancellationToken);
        menuPlan.UpdateMenuPlan(request.StartDate, request.EndDate);
        await menuPlanOverlapChecker.CheckDoesNotOverlapWithExistingPlans(menuPlan);
        await menuPlanRepository.SaveChangesAsync(cancellationToken);

        return menuPlan.Id.Value;
    }
}