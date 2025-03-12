using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition.Recipes.Dtos;
using Nutrition.Api.Contracts.Recipes;
using ProtoBuf.Grpc.Client;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.Recipes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(INutritionApiChannelFactory nutritionApiChannelFactory) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(RecipeCreationDto recipeToCreate)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var recipeClient = channel.CreateGrpcService<IRecipeService>();
        var recipeIdResponse = await recipeClient.CreateAsync(new() { Name = recipeToCreate.Name });
        return StatusCode(StatusCodes.Status201Created, recipeIdResponse.Id);
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeOverviewDto>>> GetAsync()
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var recipeClient = channel.CreateGrpcService<IRecipeService>();
        var recipes = new List<RecipeOverviewDto>();
        await foreach (var recipe in recipeClient.GetAsync())
            recipes.Add(new() { Id = recipe.Id, Name = recipe.Name });
        return Ok(recipes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetByIdAsync(Guid id)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var recipeClient = channel.CreateGrpcService<IRecipeService>();
        var recipe = await recipeClient.GetByIdAsync(new() { Id = id });
        return Ok(new RecipeDto { Name = recipe.Name });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid id, RecipeDto recipeToUpdate)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var recipeClient = channel.CreateGrpcService<IRecipeService>();
        var recipeIdResponse = await recipeClient.UpdateAsync(new() { Id = id, Name = recipeToUpdate.Name });
        return Ok(recipeIdResponse.Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        using var channel = await nutritionApiChannelFactory.CreateAsync();
        var recipeClient = channel.CreateGrpcService<IRecipeService>();
        await recipeClient.DeleteAsync(new() { Id = id });
        return NoContent();
    }
}
