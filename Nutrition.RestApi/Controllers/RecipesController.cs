using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Application.Recipes.Queries.GetRecipeDetail;

namespace Nutrition.RestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDetailDto>> GetByIdAsync(Guid id) 
    {
        var recipeDetailDto = await mediator.Send(new GetRecipeDetailQuery(id));

        return recipeDetailDto is null ? (ActionResult<RecipeDetailDto>)NotFound(id) : Ok(recipeDetailDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe(CreateRecipeCommand createRecipeCommand)
    {
        var recipeId = await mediator.Send(createRecipeCommand);

        return CreatedAtAction("GetById", new { id = recipeId }, null);
    }
}
