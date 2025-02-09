using System.ComponentModel.DataAnnotations;

namespace NutriFit.Web.Angular.BackendForFrontend.Dtos.Write;

public class RecipeWriteDto
{
    [Required]
    public string Name { get; set; } = null!;
}