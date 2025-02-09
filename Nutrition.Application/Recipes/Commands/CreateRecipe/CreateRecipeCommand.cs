using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Commands.CreateRecipe;

public class CreateRecipeCommand(string name) : ICommand<Guid>
{
    public string Name { get; } = name;
}