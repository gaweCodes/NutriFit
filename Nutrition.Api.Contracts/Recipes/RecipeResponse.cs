using ProtoBuf;

namespace Nutrition.Api.Contracts.Recipes;

[ProtoContract]
public class RecipeResponse
{
    [ProtoMember(1)]
    public string Name { get; set; } = null!;
}
