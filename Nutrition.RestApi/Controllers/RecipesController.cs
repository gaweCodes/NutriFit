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
    public async Task<Guid> CreateRecipeAsync(CreateRecipeCommandDataDto createRecipeCommandData)
    {
        var recipeId = await mediator.Send(new CreateRecipeCommand(createRecipeCommandData.Name));
        return recipeId;
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeOverviewDto>>> GetAsync()
    {
        var recipeOverviews = await mediator.Send(new GetRecipesOverviewQuery());
        return recipeOverviews;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetByIdAsync(Guid id) 
    {
        var recipeDto = await mediator.Send(new GetRecipeQuery(id));
        return recipeDto is null ? (ActionResult<RecipeDto>)NotFound(id) : Ok(recipeDto);
    }

    [HttpPut("{id}")]
    public async Task<Guid> UpdateRecipeAsync(Guid id, UpdateRecipeCommandDataDto updateRecipeCommandData)
    {
        var recipeId = await mediator.Send(new UpdateRecipeCommand(id, updateRecipeCommandData.Name));
        return recipeId;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRecipeAsync(Guid id)
    {
        await mediator.Send(new DeleteRecipeCommand(id));
        return NoContent();
    }
}
