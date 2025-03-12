using ProtoBuf;

namespace Nutrition.Api.Contracts.Common;

[ProtoContract]
public class GuidResponse
{
    [ProtoMember(1)]
    public Guid Id { get; set; }
}
