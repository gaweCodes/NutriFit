using NutriFit.ServiceDefaults;
using NutriFit.Web.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();
app.MapDefaultEndpoints();
app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
await app.RunAsync();
