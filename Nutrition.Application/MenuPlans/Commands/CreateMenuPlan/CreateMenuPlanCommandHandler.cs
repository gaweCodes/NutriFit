using Nutrition.Domain.MenuPlans;
using Nutrition.Domain.MenuPlans.Checkers;
using Nutrition.Domain.MenuPlans.Entities;
using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;

internal class CreateMenuPlanCommandHandler(IMenuPlanRepository menuPlanRepository, IUniqueMenuPlanDateRangeChecker uniqueMenuPlanDateRangeChecker) : ICommandHandler<CreateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(CreateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = MenuPlan.CreateNew(request.StartDate, request.EndDate, uniqueMenuPlanDateRangeChecker);
        await menuPlanRepository.AddAsync(menuPlan, cancellationToken);
        await menuPlanRepository.SaveChangesAsync(cancellationToken);
        return menuPlan.Id.Value;
    }
}