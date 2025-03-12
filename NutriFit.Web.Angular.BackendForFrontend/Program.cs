using NutriFit.ServiceDefaults;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddScoped<INutritionApiChannelFactory, NutritionApiChannelFactory>();

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseDefaultFiles();
app.MapStaticAssets();
app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
