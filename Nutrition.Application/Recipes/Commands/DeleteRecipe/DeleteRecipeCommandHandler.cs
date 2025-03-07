using Nutrition.Domain.Recipes;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Commands.DeleteRecipe;

internal class DeleteRecipeCommandHandler(IRecipeRepository recipeRepository) : ICommandHandler<DeleteRecipeCommand>
{
    public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipeToDelete = await recipeRepository.GetByIdAsync(new RecipeId(request.Id), cancellationToken);
        recipeToDelete.Delete();
        await recipeRepository.SaveChangesAsync(cancellationToken);
    }
}
