using Nutrition.Application.Recipes.Queries.Models;
using Nutrition.Domain.Recipes.Events;
using Nutrition.Infrastructure.Read.Database;
using SharedKernel.Domain;

namespace Nutrition.Infrastructure.Read.Projectors;

internal class RecipeProjector(NutritionReadDbContext dbContext) 
    : IDomainEventHandler<RecipeCreatedDomainEvent>, 
    IDomainEventHandler<RecipeUpdatedDomainEvent>, 
    IDomainEventHandler<RecipeIsDeletedChangedDomainEvent>
{
    public async Task Handle(RecipeCreatedDomainEvent eventData, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(RecipeDetail.CreateNew(eventData.RecipeId, eventData.Name), cancellationToken);
        await dbContext.AddAsync(RecipeOverview.CreateNew(eventData.RecipeId, eventData.Name), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(RecipeUpdatedDomainEvent eventData, CancellationToken cancellationToken)
    {
        var recipeDetail = await dbContext.FindAsync<RecipeDetail>(eventData.RecipeId, cancellationToken);
        var recipeOverview = await dbContext.FindAsync<RecipeOverview>(eventData.RecipeId, cancellationToken);

        if (recipeOverview is not null)
            recipeOverview.Name = eventData.Name;

        if (recipeDetail is not null)
            recipeDetail.Name = eventData.Name;

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(RecipeIsDeletedChangedDomainEvent eventData, CancellationToken cancellationToken)
    {
        var recipeDetail = await dbContext.FindAsync<RecipeDetail>(eventData.RecipeId, cancellationToken);
        var recipeOverview = await dbContext.FindAsync<RecipeOverview>(eventData.RecipeId, cancellationToken);

        if (recipeOverview is not null)
            dbContext.Remove(recipeOverview);

        if (recipeDetail is not null)
            dbContext.Remove(recipeDetail);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}