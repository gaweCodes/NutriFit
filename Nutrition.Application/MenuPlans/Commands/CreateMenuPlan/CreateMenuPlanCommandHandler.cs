﻿/*using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;
using SharedKernel.Application;
using SharedKernel.Domain;

namespace Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;

internal class CreateMenuPlanCommandHandler(IRepository<MenuPlan, MenuPlanId> menuPlanRepository) : ICommandHandler<CreateMenuPlanCommand, Guid>
{
    public async Task<Guid> Handle(CreateMenuPlanCommand request, CancellationToken cancellationToken)
    {
        var menuPlan = MenuPlan.CreateNew(request.StartDate, request.EndDate);
        await menuPlanRepository.StoreAsync(menuPlan, cancellationToken);
        return menuPlan.Id.Value;
    }
}*/