using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition.Recipes.Dtos;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.Recipes.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IHttpClientFactory httpFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpFactory.CreateClient("Nutrition");

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(RecipeCreationDto recipeToCreate)
    {
        var response = await _httpClient.PostAsJsonAsync("recipes", recipeToCreate);
        return StatusCode(StatusCodes.Status201Created, await response.Content.ReadFromJsonAsync<Guid>());
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeOverviewDto>>> GetAsync()
    {
        var response = await _httpClient.GetAsync("Recipes");
        return Ok(await response.Content.ReadFromJsonAsync<List<RecipeOverviewDto>>() ?? []);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"Recipes/{id}");
        return Ok(await response.Content.ReadFromJsonAsync<RecipeDto>());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid id, RecipeDto recipeToUpdate)
    {
        var response = await _httpClient.PutAsJsonAsync($"Recipes/{id}", recipeToUpdate);
        return Ok(await response.Content.ReadFromJsonAsync<Guid>());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"Recipes/{id}");
        return NoContent();
    }
}
