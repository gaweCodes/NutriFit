using Nutrition.Domain.Recipes;
using Nutrition.Domain.Recipes.Entities;
using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Commands.CreateRecipe;

internal class CreateRecipeCommandHandler(IRecipeRepository recipeRepository) : ICommandHandler<CreateRecipeCommand, Guid>
{
    public async Task<Guid> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = Recipe.CreateNew(request.Name);

        await recipeRepository.AddAsync(recipe, cancellationToken);
        await recipeRepository.SaveChangesAsync(cancellationToken);

        return recipe.Id.Value;
    }
}