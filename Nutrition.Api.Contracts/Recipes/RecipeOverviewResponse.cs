using ProtoBuf;

namespace Nutrition.Api.Contracts.Recipes;

[ProtoContract]
public class RecipeOverviewResponse
{
    [ProtoMember(1)]
    public Guid Id { get; set; }
    [ProtoMember(2)]
    public string Name { get; set; } = null!;
}
