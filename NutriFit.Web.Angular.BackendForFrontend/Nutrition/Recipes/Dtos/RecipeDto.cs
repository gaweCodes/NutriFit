using System.ComponentModel.DataAnnotations;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition.Recipes.Dtos;

public class RecipeDto 
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
