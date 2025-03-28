using Grpc.Net.ClientFactory;
using Nutrition.Api.Contracts.MenuPlans;
using Nutrition.Api.Contracts.Recipes;
using ProtoBuf.Grpc.ClientFactory;

namespace NutriFit.Web.Blazor.BackendForFrontend.Nutrition;

public static class RegisterNutritionApiClientsServiceCollectionExtensions
{
    private readonly static string ServiceName = "nutrition-api";
    public static void AddNutritionApiClients(this IServiceCollection services)
    {
        AddNutritionApiClient<IMenuPlanService>(services);
        AddNutritionApiClient<IRecipeService>(services);
    }

    private static void AddNutritionApiClient<TService>(IServiceCollection services) where TService : class
    {
        services.AddCodeFirstGrpcClient<TService>(async (sp, o) =>
        {
            var serviceUriFactory = sp.GetRequiredService<IServiceUriFactory>();
            o.Address = await serviceUriFactory.CreateAsync(ServiceName);
            o.InterceptorRegistrations.Add(new(InterceptorScope.Channel, sp => sp.GetRequiredService<ErrorHandlingInterceptor>()));
            o.ChannelOptionsActions.Add(options =>
            {
                options.HttpHandler = new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    EnableMultipleHttp2Connections = true
                };
            });
        });
    }
}
