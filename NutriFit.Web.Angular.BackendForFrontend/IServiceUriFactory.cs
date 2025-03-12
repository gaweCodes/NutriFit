namespace NutriFit.Web.Angular.BackendForFrontend;

public interface IServiceUriFactory
{
    Task<Uri> CreateAsync(string serviceName);
}