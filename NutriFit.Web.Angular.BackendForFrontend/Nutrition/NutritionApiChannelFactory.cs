using Grpc.Net.Client;
using Microsoft.Extensions.ServiceDiscovery;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition;

public class NutritionApiChannelFactory(ServiceEndpointResolver serviceEndpointResolver) : INutritionApiChannelFactory
{
    public async Task<GrpcChannel> CreateAsync()
    {
        var endpointSource = await serviceEndpointResolver.GetEndpointsAsync("https+http://nutrition-api", CancellationToken.None);
        var nutritionApiAddress = endpointSource.Endpoints[0].ToString() ?? throw new Exception("Could not resolve address");
        return GrpcChannel.ForAddress(nutritionApiAddress);
    }
}
