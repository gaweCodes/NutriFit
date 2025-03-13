using NutriFit.ServiceDefaults;
using NutriFit.Web.Angular.BackendForFrontend;
using NutriFit.Web.Angular.BackendForFrontend.Nutrition;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers(o => o.Filters.Add<HttpExceptionFilter>());

builder.Services.AddSingleton<IServiceUriFactory, ServiceUriFactory>();
builder.Services.AddSingleton<ErrorHandlingInterceptor>();
builder.Services.AddNutritionApiClients();

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseDefaultFiles();
app.MapStaticAssets();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
