using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;
using Nutrition.Application.MenuPlans.Commands.DeleteMenuPlan;
using Nutrition.Application.MenuPlans.Commands.UpdateMenuPlan;
using Nutrition.Application.MenuPlans.Queries.GetMenuPlan;
using Nutrition.Application.MenuPlans.Queries.GetMenuPlansOverview;
using Nutrition.RestApi.Dtos.MenuPlans;

namespace Nutrition.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MenuPlansController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<Guid> CreateMenuPlanAsync(CreateMenuPlanCommandDataDto createMenuPlanCommandData)
    {
        var menuPlanId = await mediator.Send(new CreateMenuPlanCommand(createMenuPlanCommandData.StartDate, createMenuPlanCommandData.EndDate, createMenuPlanCommandData.HasSnacking));
        return menuPlanId;
    }

    [HttpGet]
    public async Task<ActionResult<List<MenuPlanOverviewDto>>> GetMenuPlansAsync()
    {
        var menuPlanOverviews = await mediator.Send(new GetMenuPlansOverviewQuery());
        return menuPlanOverviews;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuPlanDto>> GetMenuPlanByIdAsync(Guid id) 
    {
        var menuPlanDto = await mediator.Send(new GetMenuPlanQuery(id));
        return menuPlanDto is null ? (ActionResult<MenuPlanDto>)NotFound(id) : Ok(menuPlanDto);
    }

    [HttpPut("{id}")]
    public async Task<Guid> UpdateMenuPlanAsync(Guid id, UpdateMenuPlanCommandDataDto updateMenuPlanCommandData)
    {
        var menuPlanId = await mediator.Send(new UpdateMenuPlanCommand(id, updateMenuPlanCommandData.StartDate, updateMenuPlanCommandData.EndDate, updateMenuPlanCommandData.HasSnacking));
        return menuPlanId;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMenuPlanAsync(Guid id)
    {
        await mediator.Send(new DeleteMenuPlanCommand(id));
        return NoContent();
    }
}
