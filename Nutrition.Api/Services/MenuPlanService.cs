using MediatR;
using Nutrition.Api.Contracts.Common;
using Nutrition.Api.Contracts.MenuPlans;
using Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;
using Nutrition.Application.MenuPlans.Commands.DeleteMenuPlan;
using Nutrition.Application.MenuPlans.Commands.UpdateMenuPlan;
using Nutrition.Application.MenuPlans.Queries.GetMenuPlan;
using Nutrition.Application.MenuPlans.Queries.GetMenuPlanOverview;
using ProtoBuf.Grpc;

namespace Nutrition.Api.Services;

public class MenuPlanService(IMediator mediator) : IMenuPlanService
{
    public async Task<GuidResponse> CreateAsync(MenuPlanCreationRequest request, CallContext context = default)
    {
        var menuPlanId = await mediator.Send(new CreateMenuPlanCommand(DateOnly.FromDateTime(request.StartDate), DateOnly.FromDateTime(request.EndDate)));
        return new() { Id = menuPlanId };
    }

    public async IAsyncEnumerable<MenuPlanOverviewResponse> GetAsync(CallContext context = default)
    {
        var menuPlanOverview = await mediator.Send(new GetMenuPlanOverviewQuery());
        foreach (var menuPlan in menuPlanOverview)
            yield return new() { Id = menuPlan.Id, StartDate = menuPlan.StartDate.ToDateTime(new TimeOnly()), EndDate = menuPlan.EndDate.ToDateTime(new TimeOnly()) };
    }

    public async Task<MenuPlanResponse> GetByIdAsync(ByIdRequest request, CallContext context = default)
    {
        var menuPlan = await mediator.Send(new GetMenuPlanQuery(request.Id));
        return new() { StartDate = menuPlan.StartDate.ToDateTime(new TimeOnly()), EndDate = menuPlan.EndDate.ToDateTime(new TimeOnly()) };
    }

    public async Task<GuidResponse> UpdateAsync(MenuPlanUpdateRequest request, CallContext context = default)
    {
        var menuPlanId = await mediator.Send(new UpdateMenuPlanCommand(request.Id, DateOnly.FromDateTime(request.StartDate), DateOnly.FromDateTime(request.EndDate)));
        return new() { Id = menuPlanId };
    }

    public async Task DeleteAsync(ByIdRequest request, CallContext context = default)
    {
        await mediator.Send(new DeleteMenuPlanCommand(request.Id));
    }
}
