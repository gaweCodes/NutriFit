using Microsoft.AspNetCore.Mvc;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Dtos;
using System.Text.Json;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.MenuPlans.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuPlansController(IHttpClientFactory httpFactory) : ControllerBase
{
    private readonly HttpClient _httpClient = httpFactory.CreateClient("Nutrition");

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(MenuPlanCreationDto menuPlanToCreate)
    {
        var response = await _httpClient.PostAsJsonAsync("menuplans", menuPlanToCreate);
        return StatusCode(StatusCodes.Status201Created, await response.Content.ReadFromJsonAsync<Guid>());
    }

    [HttpGet]
    public async Task<ActionResult<List<MenuPlanOverviewDto>>> GetAsync()
    {
        var response = await _httpClient.GetAsync("menuplans");
        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var overviewList = doc.RootElement.EnumerateArray()
            .Select(x => new MenuPlanOverviewDto
            {
                Id = x.GetProperty("id").GetGuid(),
                Period = $"{x.GetProperty("startDate").GetDateTime().Date:dd.MM.yyyy} - {x.GetProperty("endDate").GetDateTime().Date:dd.MM.yyyy}"
            })
            .ToList();
        
        return Ok(overviewList);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MenuPlanDto>> GetByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"menuplans/{id}");
        return Ok((await response.Content.ReadFromJsonAsync<MenuPlanDto>())!);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateAsync(Guid id, MenuPlanDto menuPlanToUpdate)
    {
        var response = await _httpClient.PutAsJsonAsync($"menuplans/{id}", menuPlanToUpdate);
        return Ok(await response.Content.ReadFromJsonAsync<Guid>());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"menuplans/{id}");
        return NoContent();
    }
}
