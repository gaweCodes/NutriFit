using NutriFit.ServiceDefaults;
using Nutrition.Application.Recipes.Commands.CreateRecipe;
using Nutrition.Domain.MenuPlans.Checkers;
using Nutrition.Infrastructure;
using Nutrition.RestApi;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(CreateRecipeCommand).Assembly, typeof(NutritionInfrastructureServiceColectionExtensions).Assembly));
builder.Services.AddNutritionInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapDefaultEndpoints();
if (app.Environment.IsDevelopment()) app.MapOpenApi();
app.UseHttpsRedirection();
app.MapControllers();
app.UseRouting();

app.Run();
