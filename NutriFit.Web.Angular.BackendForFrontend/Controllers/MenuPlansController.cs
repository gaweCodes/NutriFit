using Microsoft.AspNetCore.Mvc;

namespace NutriFit.Web.Angular.BackendForFrontend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuPlansController : ControllerBase
{
    private readonly ILogger<MenuPlansController> _logger;
    private readonly HttpClient _httpClient;

    public MenuPlansController(ILogger<MenuPlansController> logger, IHttpClientFactory httpFactory)
    {
        _logger = logger;
        _httpClient = httpFactory.CreateClient("Nutrition");
    }

    [HttpGet]
    public async Task<List<WeatherForecast>?> Get()
    {
        var response = await _httpClient.GetAsync("weatherforecast");
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("The request to the Core API failed. Status Code: {statusCode}", response.StatusCode);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<List<WeatherForecast>>();
    }
}
