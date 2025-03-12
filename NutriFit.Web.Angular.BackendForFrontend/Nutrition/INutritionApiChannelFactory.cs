using Grpc.Net.Client;

namespace NutriFit.Web.Angular.BackendForFrontend.Nutrition;

public interface INutritionApiChannelFactory
{
    Task<GrpcChannel> CreateAsync();
}