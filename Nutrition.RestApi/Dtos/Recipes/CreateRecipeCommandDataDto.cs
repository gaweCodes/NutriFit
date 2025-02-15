using System.ComponentModel.DataAnnotations;

namespace Nutrition.RestApi.Dtos.Recipes;

public class CreateRecipeCommandDataDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
