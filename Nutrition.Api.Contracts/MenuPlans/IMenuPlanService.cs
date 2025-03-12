using Nutrition.Api.Contracts.Common;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Nutrition.Api.Contracts.MenuPlans;

[Service]
public interface IMenuPlanService
{
    [Operation]
    Task<GuidResponse> CreateAsync(MenuPlanCreationRequest request, CallContext context = default);
    [Operation]
    IAsyncEnumerable<MenuPlanOverviewResponse> GetAsync(CallContext context = default);
    [Operation]
    Task<MenuPlanResponse> GetByIdAsync(ByIdRequest request, CallContext context = default);
    [Operation]
    Task<GuidResponse> UpdateAsync(MenuPlanUpdateRequest request, CallContext context = default);
    [Operation]
    Task DeleteAsync(ByIdRequest request, CallContext context = default);
}
