using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;
using SharedKernel.Application;
using SharedKernel.Domain;

namespace Nutrition.Application.MenuPlans.Commands.DeleteMenuPlan;

internal class DeleteMenuPlanCommandHandler(IRepository<MenuPlan, MenuPlanId> menuPlanRepository) : ICommandHandler<DeleteMenuPlanCommand>
{
    public async Task Handle(DeleteMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = await menuPlanRepository.GetSpecificAsync(new MenuPlanId(request.Id), cancellationToken);
        menuPlan.Delete();
        await menuPlanRepository.StoreAsync(menuPlan, cancellationToken);
    }
}
