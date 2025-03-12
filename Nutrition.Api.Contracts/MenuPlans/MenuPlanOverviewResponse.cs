using ProtoBuf;

namespace Nutrition.Api.Contracts.MenuPlans;

[ProtoContract]
public class MenuPlanOverviewResponse
{
    [ProtoMember(1)]
    public Guid Id { get; set; }
    [ProtoMember(2)]
    public DateTime StartDate { get; set; }
    [ProtoMember(3)]
    public DateTime EndDate { get; set; }
}
