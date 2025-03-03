using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Application.Recipes.Commands.DeleteRecipe;
using Nutrition.Application.Recipes.Commands.UpdateRecipe;
using Nutrition.Application.Recipes.Queries.GetRecipe;
using Nutrition.Application.Recipes.Queries.GetRecipesOverview;
using Nutrition.RestApi.Dtos.Recipes;

namespace Nutrition.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateRecipeAsync(CreateRecipeCommandDataDto createRecipeCommandData) => 
        StatusCode(StatusCodes.Status201Created, await mediator.Send(new CreateRecipeCommand(createRecipeCommandData.Name)));

    [HttpGet]
    public async Task<ActionResult<List<RecipeOverviewDto>>> GetRecipesAsync() => 
        Ok(await mediator.Send(new GetRecipesOverviewQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetRecipeByIdAsync(Guid id) => 
        Ok(await mediator.Send(new GetRecipeQuery(id)));

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateRecipeAsync(Guid id, UpdateRecipeCommandDataDto updateRecipeCommandData) => 
        Ok(await mediator.Send(new UpdateRecipeCommand(id, updateRecipeCommandData.Name)));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRecipeAsync(Guid id)
    {
        await mediator.Send(new DeleteRecipeCommand(id));
        return NoContent();
    }
}
