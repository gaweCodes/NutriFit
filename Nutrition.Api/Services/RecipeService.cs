using MediatR;
using ProtoBuf.Grpc;
using Nutrition.Api.Contracts.Recipes;
using Nutrition.Api.Contracts.Common;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Application.Recipes.Queries.GetRecipeOverview;
using Nutrition.Application.Recipes.Queries.GetRecipe;
using Nutrition.Application.Recipes.Commands.UpdateRecipe;
using Nutrition.Application.Recipes.Commands.DeleteRecipe;

namespace Nutrition.Api.Services;

public class RecipeService(IMediator mediator) : IRecipeService
{
    public async Task<GuidResponse> CreateAsync(RecipeCreationRequest request, CallContext context = default)
    {
        var recipeId = await mediator.Send(new CreateRecipeCommand(request.Name));
        return new() { Id = recipeId };
    }
    
    public async IAsyncEnumerable<RecipeOverviewResponse> GetAsync(CallContext context = default)
    {
        var recipeOverview = await mediator.Send(new GetRecipeOverviewQuery());
        foreach (var recipe in recipeOverview)
            yield return new() { Id = recipe.Id, Name = recipe.Name };
    }

    public async Task<RecipeResponse> GetByIdAsync(ByIdRequest request, CallContext context = default)
    {
        var recipe = await mediator.Send(new GetRecipeQuery(request.Id));
        return new() { Name = recipe.Name };
    }

    public async Task<GuidResponse> UpdateAsync(RecipeUpdateRequest request, CallContext context = default)
    {
        var recipeId = await mediator.Send(new UpdateRecipeCommand(request.Id, request.Name));
        return new() { Id = recipeId };
    }

    public async Task DeleteAsync(ByIdRequest request, CallContext context = default)
    {
        await mediator.Send(new DeleteRecipeCommand(request.Id));
    }
}
