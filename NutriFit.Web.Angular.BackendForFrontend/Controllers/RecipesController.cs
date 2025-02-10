using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Dtos.Read;
using NutriFit.Web.Angular.BackendForFrontend.Dtos.Write;

namespace NutriFit.Web.Angular.BackendForFrontend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public RecipesController(IHttpClientFactory httpFactory)
    {
        _httpClient = httpFactory.CreateClient("Nutrition");
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Post(RecipeWriteDto recipeWriteDto)
    {
        var response = await _httpClient.PostAsJsonAsync("recipes", recipeWriteDto);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<RecipeDetailDto?> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"Recipes/{id}");

        return await response.Content.ReadFromJsonAsync<RecipeDetailDto>();
    }
}
