using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Commands.UpdateRecipe;

public class UpdateRecipeCommand(Guid id, string name) : ICommand<Guid>
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}