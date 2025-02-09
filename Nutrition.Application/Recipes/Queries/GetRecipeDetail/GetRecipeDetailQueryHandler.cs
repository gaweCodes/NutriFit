using Nutrition.Infrastructure.Read.Database;
using Nutrition.Infrastructure.Read.DatabaseObjects;
using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipeDetail;

internal class GetRecipeDetailQueryHandler(NutritionReadDbContext dbContext) : IQueryHandler<GetRecipeDetailQuery, RecipeDetailDto?>
{
    public async Task<RecipeDetailDto?> Handle(GetRecipeDetailQuery query, CancellationToken cancellationToken)
    {
        var recipeDetail = await dbContext.FindAsync< RecipeDetail>(query.Id, cancellationToken);
        
        return recipeDetail is not null ? new RecipeDetailDto(recipeDetail.Id, recipeDetail.Name) : null;
    }
}