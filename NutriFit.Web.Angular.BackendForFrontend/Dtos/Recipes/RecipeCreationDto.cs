using System.ComponentModel.DataAnnotations;

namespace NutriFit.Web.Angular.BackendForFrontend.Dtos.Recipes;

public class RecipeCreationDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}