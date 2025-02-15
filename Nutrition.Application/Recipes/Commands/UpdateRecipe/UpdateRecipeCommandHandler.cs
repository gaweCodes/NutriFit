using Nutrition.Domain.Recipes;
using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Commands.UpdateRecipe;

internal class UpdateRecipeCommandHandler(IRecipeRepository recipeRepository) : ICommandHandler<UpdateRecipeCommand, Guid>
{
    public async Task<Guid> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.GetByIdAsync(new RecipeId(request.Id), cancellationToken);
        recipe.UpdateRecipe(request.Name);
        await recipeRepository.SaveChangesAsync(cancellationToken);

        return recipe.Id.Value;
    }
}