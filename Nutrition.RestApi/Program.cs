using NutriFit.ServiceDefaults;
using Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateRecipeCommand).Assembly, typeof(NutritionInfrastructureServiceColectionExtensions).Assembly));
builder.Services.AddNutritionInfrastructure(builder.Configuration);

var app = builder.Build();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment()) app.MapOpenApi();
app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();

app.Run();
