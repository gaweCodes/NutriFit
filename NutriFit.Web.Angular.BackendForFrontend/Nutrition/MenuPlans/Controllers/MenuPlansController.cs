using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Dtos;
using Nutrition.Api.Contracts.MenuPlans;
using ProtoBuf.Grpc.Client;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuPlansController(INutritionApiChannelFactory nutritionApiChannelFactory) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(MenuPlanCreationDto menuPlanToCreate)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var menuPlanClient = channel.CreateGrpcService<IMenuPlanService>();
        var menuPlanIdResponse = await menuPlanClient.CreateAsync(new() { StartDate = menuPlanToCreate.StartDate.ToDateTime(new TimeOnly()), EndDate = menuPlanToCreate.EndDate.ToDateTime(new TimeOnly()) });
        return StatusCode(StatusCodes.Status201Created, menuPlanIdResponse.Id);
    }

    [HttpGet]
    public async Task<ActionResult<List<MenuPlanOverviewDto>>> GetAsync()
    {

        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var menuPlanClient = channel.CreateGrpcService<IMenuPlanService>();
        var menuPlanOverview = new List<MenuPlanOverviewDto>();
        await foreach (var menuPlan in menuPlanClient.GetAsync())
        {
            menuPlanOverview.Add(new() { Id = menuPlan.Id, Period = $"{menuPlan.StartDate:dd.MM.yyyy} - {menuPlan.EndDate:dd.MM.yyyy}" });
        }
        
        return Ok(menuPlanOverview);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuPlanDto>> GetByIdAsync(Guid id)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var menuPlanClient = channel.CreateGrpcService<IMenuPlanService>();
        var menuPlanResponse = await menuPlanClient.GetByIdAsync(new() { Id = id });
        return Ok(new MenuPlanDto() { StartDate = DateOnly.FromDateTime(menuPlanResponse.StartDate), EndDate = DateOnly.FromDateTime(menuPlanResponse.EndDate) });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid id, MenuPlanDto menuPlanToUpdate)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var menuPlanClient = channel.CreateGrpcService<IMenuPlanService>();
        var idResponse = await menuPlanClient.UpdateAsync(new() { Id = id, StartDate = menuPlanToUpdate.StartDate.ToDateTime(new TimeOnly()), EndDate = menuPlanToUpdate.EndDate.ToDateTime(new TimeOnly()) });
        return Ok(idResponse.Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var menuPlanClient = channel.CreateGrpcService<IMenuPlanService>();
        await menuPlanClient.DeleteAsync(new() { Id= id });
        return NoContent();
    }
}
