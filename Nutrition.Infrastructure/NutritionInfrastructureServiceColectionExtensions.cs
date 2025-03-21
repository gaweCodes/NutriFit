using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Application.MenuPlans.Queries;
using Nutrition.Application.Recipes.Queries;
using Nutrition.Domain.Recipes.Entities;
using Nutrition.Domain.Recipes.States;
using Nutrition.Domain.Recipes.ValueObjects;
using Nutrition.Infrastructure.Read.Database;
using Nutrition.Infrastructure.Read.Repositories;
using SharedKernel.Domain;
using SharedKernel.Infrastructure;

namespace Nutrition.Infrastructure;

public static class NutritionInfrastructureServiceColectionExtensions
{
    public static IServiceCollection AddNutritionInfrastructure(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        serviceCollection.AddEventStoreClient(configuration.GetConnectionString("event-store")!);
        serviceCollection.AddScoped<IRepository<Recipe, RecipeId>, Repository<Recipe, RecipeState, RecipeId>>();
        //serviceCollection.AddScoped<IRepository<MenuPlan, MenuPlanId>, Repository<MenuPlan, MenuPlanId>>();
        serviceCollection.AddScoped<IReadRecipeRepository, ReadRecipeRepository>();
        serviceCollection.AddScoped<IReadMenuPlanRepository, ReadMenuPlanRepository>();
        serviceCollection.AddDbContext<NutritionReadDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("nutrition-read")));

        return serviceCollection;
    }
}
