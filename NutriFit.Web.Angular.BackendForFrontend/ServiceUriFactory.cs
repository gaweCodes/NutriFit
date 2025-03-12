using Microsoft.Extensions.ServiceDiscovery;

namespace NutriFit.Web.Angular.BackendForFrontend;

public class ServiceUriFactory(ServiceEndpointResolver serviceEndpointResolver) : IServiceUriFactory
{
    public async Task<Uri> CreateAsync(string serviceName)
    {
        var endpointSource = await serviceEndpointResolver.GetEndpointsAsync($"https+http://{serviceName}", CancellationToken.None);
        var serviceAddress = endpointSource.Endpoints[0].ToString() ?? throw new Exception($"Could not resolve address for service: {serviceName}");
        return new Uri(serviceAddress);
    }
}
