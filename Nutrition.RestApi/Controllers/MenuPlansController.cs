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
    public async Task<ActionResult<Guid>> CreateMenuPlanAsync(CreateMenuPlanCommandDataDto createMenuPlanCommandData)
    {
        var menuPlanId = await mediator.Send(new CreateMenuPlanCommand(createMenuPlanCommandData.StartDate, createMenuPlanCommandData.EndDate));
        return StatusCode(StatusCodes.Status201Created, menuPlanId);
    }

    [HttpGet]
    public async Task<ActionResult<List<MenuPlanOverviewDto>>> GetMenuPlansAsync() => 
        Ok(await mediator.Send(new GetMenuPlansOverviewQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuPlanDto>> GetMenuPlanByIdAsync(Guid id) => 
        Ok(await mediator.Send(new GetMenuPlanQuery(id)));

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateMenuPlanAsync(Guid id, UpdateMenuPlanCommandDataDto updateMenuPlanCommandData) => 
        Ok(await mediator.Send(new UpdateMenuPlanCommand(id, updateMenuPlanCommandData.StartDate, updateMenuPlanCommandData.EndDate)));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMenuPlanAsync(Guid id)
    {
        await mediator.Send(new DeleteMenuPlanCommand(id));
        return NoContent();
    }
}
