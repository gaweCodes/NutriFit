using ProtoBuf;

namespace Nutrition.Api.Contracts.MenuPlans;

[ProtoContract]
public class MenuPlanResponse
{
    [ProtoMember(1)]
    public DateTime StartDate { get; set; }
    [ProtoMember(2)]
    public DateTime EndDate { get; set; }
}
