using Nutrition.Domain.Recipes.Events;
using Nutrition.Domain.Recipes.States;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.Entities;

public class Recipe : Entity<RecipeState, RecipeId>, IAggregateRoot<Recipe, RecipeState, RecipeId>
{
    private Recipe() { }
    private Recipe(RecipeState state) => State = state;
    public static Recipe CreateNew(string name)
    {
        var recipe = new Recipe(new RecipeState());
        recipe.RaiseEvent(new RecipeCreatedDomainEvent(Guid.NewGuid(), name));
        return recipe;
    }
    public static Recipe FromState(RecipeState state) => new Recipe(state);
    public void UpdateRecipe(string name)
    {
        RaiseEvent(new RecipeUpdatedDomainEvent(Id.Value, name));
    }

    public void Delete()
    {
        RaiseEvent(new RecipeIsDeletedChangedDomainEvent(Id.Value, true));
    }
}
