using Nutrition.Api.Contracts.Common;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Nutrition.Api.Contracts.Recipes;

[Service]
public interface IRecipeService
{
    [Operation]
    Task<GuidResponse> CreateAsync(RecipeCreationRequest request, CallContext context = default);
    [Operation]
    IAsyncEnumerable<RecipeOverviewResponse> GetAsync(CallContext context = default);
    [Operation]
    Task<RecipeResponse> GetByIdAsync(ByIdRequest request, CallContext context = default);
    [Operation]
    Task<GuidResponse> UpdateAsync(RecipeUpdateRequest request, CallContext context = default);
    [Operation]
    Task DeleteAsync(ByIdRequest request, CallContext context = default);
}
