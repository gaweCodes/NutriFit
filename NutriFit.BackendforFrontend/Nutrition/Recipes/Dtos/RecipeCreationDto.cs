using System.ComponentModel.DataAnnotations;

namespace NutriFit.BackendForFrontend.Nutrition.Recipes.Dtos;

public class RecipeCreationDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}