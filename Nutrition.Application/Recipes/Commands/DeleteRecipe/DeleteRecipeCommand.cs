using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Commands.DeleteRecipe;

public class DeleteRecipeCommand(Guid id) : ICommand
{
    public Guid Id { get; } = id;
}
