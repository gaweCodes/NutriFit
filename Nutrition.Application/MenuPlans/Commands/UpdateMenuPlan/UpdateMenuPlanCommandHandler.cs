/*using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;
using SharedKernel.Application;
using SharedKernel.Domain;

namespace Nutrition.Application.MenuPlans.Commands.UpdateMenuPlan;

internal class UpdateMenuPlanCommandHandler(IRepository<MenuPlan, MenuPlanId> menuPlanRepository) : ICommandHandler<UpdateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(UpdateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = await menuPlanRepository.GetSpecificAsync(new MenuPlanId(request.Id), cancellationToken);
        menuPlan.UpdateMenuPlan(request.StartDate, request.EndDate);
        await menuPlanRepository.StoreAsync(menuPlan, cancellationToken);
        return menuPlan.Id.Value;
    }
}*/