using Nutrition.Domain.MenuPlans;
using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;

internal class CreateMenuPlanCommandHandler(IMenuPlanRepository menuPlanRepository) : ICommandHandler<CreateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(CreateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = MenuPlan.CreateNew(request.StartDate, request.EndDate, request.HasSnacking);

        await menuPlanRepository.AddAsync(menuPlan, cancellationToken);
        await menuPlanRepository.SaveChangesAsync(cancellationToken);

        return menuPlan.Id.Value;
    }
}