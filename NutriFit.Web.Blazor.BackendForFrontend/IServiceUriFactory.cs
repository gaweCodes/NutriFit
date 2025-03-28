namespace NutriFit.Web.Blazor.BackendForFrontend;

public interface IServiceUriFactory
{
    Task<Uri> CreateAsync(string serviceName);
}