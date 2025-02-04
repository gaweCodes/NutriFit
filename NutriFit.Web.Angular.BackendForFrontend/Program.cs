using NutriFit.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddHttpClient("NutriFitCoreClient", static client => client.BaseAddress = new("https+http://nutrifit-core"));

var app = builder.Build();
app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.MapStaticAssets();

app.UseHttpsRedirection();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
