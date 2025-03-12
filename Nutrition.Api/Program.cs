using NutriFit.ServiceDefaults;
using Nutrition.Api.Services;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Infrastructure;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateRecipeCommand).Assembly, typeof(NutritionInfrastructureServiceColectionExtensions).Assembly));
builder.Services.AddNutritionInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints();
app.MapGrpcService<MenuPlanService>();
app.MapGrpcService<RecipeService>();
app.Run();
