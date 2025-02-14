using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Dtos.Read;
using NutriFit.Web.Angular.BackendForFrontend.Dtos.Write;

namespace NutriFit.Web.Angular.BackendForFrontend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController(IHttpClientFactory httpFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpFactory.CreateClient("Nutrition");

    [HttpGet]
    public async Task<List<RecipeOverviewDto>> Get()
    {
        var response = await _httpClient.GetAsync("Recipes");
        return (await response.Content.ReadFromJsonAsync<List<RecipeOverviewDto>>()) ?? [];
    }

    [HttpGet("{id}")]
    public async Task<RecipeDetailDto> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"Recipes/{id}");
        return (await response.Content.ReadFromJsonAsync<RecipeDetailDto>())!;
    }

    [HttpPost]
    public async Task<Guid> Post(RecipeWriteDto recipeWriteDto)
    {
        var response = await _httpClient.PostAsJsonAsync("recipes", recipeWriteDto);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }
}
