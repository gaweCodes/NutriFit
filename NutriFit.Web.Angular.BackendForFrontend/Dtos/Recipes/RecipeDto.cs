using System.ComponentModel.DataAnnotations;

namespace NutriFit.Web.Angular.BackendForFrontend.Dtos.Recipes;

public class RecipeDto 
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
