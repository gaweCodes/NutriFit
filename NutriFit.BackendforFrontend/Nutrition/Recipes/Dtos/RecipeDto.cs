using System.ComponentModel.DataAnnotations;

namespace NutriFit.BackendForFrontend.Nutrition.Recipes.Dtos;

public class RecipeDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
