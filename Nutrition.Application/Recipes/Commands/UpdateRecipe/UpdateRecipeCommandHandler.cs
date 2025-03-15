using Nutrition.Domain.Recipes.Entities;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Application;
using SharedKernel.Domain;

namespace Nutrition.Application.Recipes.Commands.UpdateRecipe;

internal class UpdateRecipeCommandHandler(IRepository<Recipe, RecipeId> recipeRepository) : ICommandHandler<UpdateRecipeCommand, Guid>
{
    public async Task<Guid> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.GetSpecificAsync(new RecipeId(request.Id), cancellationToken);
        recipe.UpdateRecipe(request.Name);
        await recipeRepository.StoreAsync(recipe, recipe.Id, cancellationToken);
        return recipe.Id.Value;
    }
}