using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nutrition.Infrastructure.Read.Database;
using SharedKernel.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Nutrition.Application.Recipes.Queries;
using Nutrition.Infrastructure.Read.Repositories;
using Nutrition.Application.MenuPlans.Queries;
using Marten;
using SharedKernel.Domain;
using Nutrition.Domain.Recipes.Entities;
using Nutrition.Domain.Recipes.ValueObjects;
using Nutrition.Domain.MenuPlans.ValueObjects;
using Nutrition.Domain.MenuPlans.Entities;
using Weasel.Core;

namespace Nutrition.Infrastructure;

public static class NutritionInfrastructureServiceColectionExtensions
{
    public static IServiceCollection AddNutritionInfrastructure(this IServiceCollection serviceCollection, ConfigurationManager configuration)
    {
        serviceCollection.AddScoped<IRepository<Recipe, RecipeId>, Repository<Recipe, RecipeId>>();
        serviceCollection.AddScoped<IRepository<MenuPlan, MenuPlanId>, Repository<MenuPlan, MenuPlanId>>();
        serviceCollection.AddMarten(o =>
        {   
            o.Connection(configuration.GetConnectionString("nutrition-events")!);
            o.UseSystemTextJsonForSerialization();
            o.AutoCreateSchemaObjects = AutoCreate.All;
            o.DataSourceFactory(new CustomNpgSqlDataSourceFactory());
        });
        serviceCollection.AddScoped<IReadRecipeRepository, ReadRecipeRepository>();
        serviceCollection.AddScoped<IReadMenuPlanRepository, ReadMenuPlanRepository>();
        serviceCollection.AddDbContext<NutritionReadDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("nutrition-read")));

        return serviceCollection;
    }
}
