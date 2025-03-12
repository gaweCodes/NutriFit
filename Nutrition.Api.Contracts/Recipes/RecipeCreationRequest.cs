using ProtoBuf;

namespace Nutrition.Api.Contracts.Recipes;

[ProtoContract]
public class RecipeCreationRequest
{
    [ProtoMember(1)]
    public string Name { get; set; } = null!;
}
