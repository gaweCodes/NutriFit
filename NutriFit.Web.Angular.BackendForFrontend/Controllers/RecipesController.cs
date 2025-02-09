using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<List<WeatherForecast>?> Get()
    {
        var response = await _httpClient.PostAsJsonAsync("Recipes", new RecipeWriteDto { Name = "asdf" });

        return [];
    }
}
