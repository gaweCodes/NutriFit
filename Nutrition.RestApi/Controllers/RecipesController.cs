using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Application.Recipes.Queries.GetRecipeDetail;
using Nutrition.Application.Recipes.Queries.GetRecipesOverview;

namespace Nutrition.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<RecipeOverviewDto>>> GetAsync()
    {
        var recipeOverviews = await mediator.Send(new GetRecipesOverviewQuery());
        
        return recipeOverviews;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDetailDto>> GetByIdAsync(Guid id) 
    {
        var recipeDetailDto = await mediator.Send(new GetRecipeDetailQuery(id));
        return recipeDetailDto is null ? (ActionResult<RecipeDetailDto>)NotFound(id) : Ok(recipeDetailDto);
    }

    [HttpPost]
    public async Task<Guid> CreateRecipeAsync(CreateRecipeCommand createRecipeCommand)
    {
        var recipeId = await mediator.Send(createRecipeCommand);
        return recipeId;
    }
}
