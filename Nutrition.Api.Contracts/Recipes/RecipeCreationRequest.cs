using ProtoBuf;

namespace Nutrition.Api.Contracts.Recipes;

[ProtoContract]
public class RecipeCreationRequest
{
    [ProtoMember(1, IsRequired = true)]
    public string Name { get; set; } = null!;
}
