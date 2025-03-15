using ProtoBuf;

namespace Nutrition.Api.Contracts.Common;

[ProtoContract]
public class ByIdRequest
{
    [ProtoMember(1)]
    public Guid Id { get; set; }
}