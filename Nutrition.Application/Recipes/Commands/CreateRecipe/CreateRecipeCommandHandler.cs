using Nutrition.Domain.Recipes;
using Nutrition.Domain.Recipes.Entities;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Application;
using SharedKernel.Domain;

namespace Nutrition.Application.Recipes.Commands.CreateRecipe;

internal class CreateRecipeCommandHandler(IRepository<Recipe, RecipeId> recipeRepository) : ICommandHandler<CreateRecipeCommand, Guid>
{
    public async Task<Guid> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = Recipe.CreateNew(request.Name);
        await recipeRepository.StoreAsync(recipe, recipe.Id, cancellationToken);
        return recipe.Id.Value;
    }
}