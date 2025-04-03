namespace NutriFit.BackendForFrontend;

public interface IServiceUriFactory
{
    Task<Uri> CreateAsync(string serviceName);
}