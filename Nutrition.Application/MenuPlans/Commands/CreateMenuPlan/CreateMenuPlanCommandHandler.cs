using Nutrition.Domain.MenuPlans;
using Nutrition.Domain.MenuPlans.Services;
using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;

internal class CreateMenuPlanCommandHandler(IMenuPlanRepository menuPlanRepository, MenuPlanOverlapChecker menuPlanOverlapChecker) : ICommandHandler<CreateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(CreateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = MenuPlan.CreateNew(request.StartDate, request.EndDate);
        
        await menuPlanOverlapChecker.CheckDoesNotOverlapWithExistingPlans(menuPlan);

        await menuPlanRepository.AddAsync(menuPlan, cancellationToken);
        await menuPlanRepository.SaveChangesAsync(cancellationToken);
        return menuPlan.Id.Value;
    }
}