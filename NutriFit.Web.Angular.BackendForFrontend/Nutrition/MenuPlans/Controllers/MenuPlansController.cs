using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Dtos;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuPlansController(IHttpClientFactory httpFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpFactory.CreateClient("Nutrition");

    [HttpPost]
    public async Task<Guid> CreateAsync(MenuPlanCreationDto menuPlanToCreate)
    {
        var response = await _httpClient.PostAsJsonAsync("menuplans", menuPlanToCreate);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    [HttpGet]
    public async Task<List<MenuPlanOverviewDto>> GetAsync()
    {
        var response = await _httpClient.GetAsync("menuplans");
        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return [.. doc.RootElement.EnumerateArray()
            .Select(x => new MenuPlanOverviewDto
            {
                Id = x.GetProperty("id").GetGuid(),
                Period = $"{x.GetProperty("startDate").GetDateTime().Date:dd.MM.yyyy} - {x.GetProperty("endDate").GetDateTime().Date:dd.MM.yyyy}"
            })];
    }

    [HttpGet("{id}")]
    public async Task<MenuPlanDto> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"menuplans/{id}");
        return (await response.Content.ReadFromJsonAsync<MenuPlanDto>())!;
    }

    [HttpPut("{id}")]
    public async Task<Guid> UpdateAsync(Guid id, MenuPlanDto menuPlanToUpdate)
    {
        var response = await _httpClient.PutAsJsonAsync($"menuplans/{id}", menuPlanToUpdate);
        return await response.Content.ReadFromJsonAsync<Guid>();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"menuplans/{id}");
        return NoContent();
    }
}
