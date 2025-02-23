using System.ComponentModel.DataAnnotations;

namespace Nutrition.RestApi.Dtos.Recipes;

public class UpdateRecipeCommandDataDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
