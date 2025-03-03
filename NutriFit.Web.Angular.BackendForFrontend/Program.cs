using NutriFit.ServiceDefaults;
using NutriFit.Web.Angular.BackendForFrontend;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddHttpClient("Nutrition", static client => client.BaseAddress = new("https+http://nutrition-rest-api"))
    .AddHttpMessageHandler<ApiErrorHandler>();
builder.Services.AddScoped<ApiErrorHandler>();

var app = builder.Build();
app.UseMiddleware<HttpRequestExceptionMiddleware>();
app.MapDefaultEndpoints();
app.UseDefaultFiles();
app.MapStaticAssets();
app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
