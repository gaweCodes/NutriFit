using Nutrition.Domain.MenuPlans;
using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.DeleteMenuPlan;

internal class DeleteMenuPlanCommandHandler(IMenuPlanRepository menuPlanRepository) : ICommandHandler<DeleteMenuPlanCommand>
{
    public async Task Handle(DeleteMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlanToDelete = await menuPlanRepository.GetByIdAsync(new MenuPlanId(request.Id), cancellationToken);
        menuPlanToDelete.Delete();
        await menuPlanRepository.SaveChangesAsync(cancellationToken);
    }
}
