using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Domain.Recipes;
using Nutrition.Infrastructure.Read.Database;
using Nutrition.Infrastructure.Write.Database;
using Nutrition.Infrastructure.Write.Repositories;
using SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Nutrition.Application.Recipes.Queries;
using Nutrition.Infrastructure.Read.Repositories;
using Nutrition.Domain.MenuPlans;
using Nutrition.Application.MenuPlans.Queries;

namespace Nutrition.Infrastructure;

public static class NutritionInfrastructureServiceColectionExtensions
{
    public static IServiceCollection AddNutritionInfrastructure(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        serviceCollection.AddScoped<IRecipeRepository, RecipeRepository>();
        serviceCollection.AddScoped<IMenuPlanRepository, MenuPlanRepository>();
        serviceCollection.AddScoped<IReadRecipeRepository, ReadRecipeRepository>();
        serviceCollection.AddScoped<IReadMenuPlanRepository, ReadMenuPlanRepository>();
        serviceCollection.AddDbContext<NutritionWriteDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString("nutrition-write"));
            options.AddInterceptors(new AfterSaveInterceptor(sp));
        });
        serviceCollection.AddDbContext<NutritionReadDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("nutrition-read")));
        
        return serviceCollection;
    }
}
