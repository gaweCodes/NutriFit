using Nutrition.Application.Recipes.Queries.Models;
using Nutrition.Domain.Recipes.Events;
using Nutrition.Infrastructure.Read.Database;
using SharedKernel.Domain;

namespace Nutrition.Infrastructure.Read.Projectors;

internal class RecipeProjector(NutritionReadDbContext dbContext) : IDomainEventHandler<RecipeCreatedDomainEvent>
{
    public async Task Handle(RecipeCreatedDomainEvent eventData, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(RecipeDetail.CreateNew(eventData.RecipeId, eventData.Name), cancellationToken);
        await dbContext.AddAsync(RecipeOverview.CreateNew(eventData.RecipeId, eventData.Name), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
