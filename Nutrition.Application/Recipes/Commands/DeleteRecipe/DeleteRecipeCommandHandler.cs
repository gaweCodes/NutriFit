using Nutrition.Domain.Recipes.Entities;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Application;
using SharedKernel.Domain;

namespace Nutrition.Application.Recipes.Commands.DeleteRecipe;

internal class DeleteRecipeCommandHandler(IRepository<Recipe, RecipeId> recipeRepository) : ICommandHandler<DeleteRecipeCommand>
{
    public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.GetSpecificAsync(new RecipeId(request.Id), cancellationToken);
        recipe.Delete();
        await recipeRepository.StoreAsync(recipe, recipe.Id, cancellationToken);
    }
}
