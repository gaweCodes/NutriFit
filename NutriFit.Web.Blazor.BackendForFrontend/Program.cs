using NutriFit.ServiceDefaults;
using NutriFit.Web.Blazor.BackendForFrontend;
using NutriFit.Web.Blazor.BackendForFrontend.Nutrition;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers(o => o.Filters.Add<HttpExceptionFilter>());
builder.Services.AddSingleton<IServiceUriFactory, ServiceUriFactory>();
builder.Services.AddSingleton<ErrorHandlingInterceptor>();
builder.Services.AddNutritionApiClients();

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseHttpsRedirection();
app.MapControllers();
await app.RunAsync();
