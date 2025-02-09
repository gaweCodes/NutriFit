using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Infrastructure.Read.DatabaseObjects;

namespace Nutrition.Infrastructure.Read.DatabaseObjectConfigurations;

internal class RecipeDetailConfiguration : IEntityTypeConfiguration<RecipeDetail>
{
    public void Configure(EntityTypeBuilder<RecipeDetail> builder)
    {
        builder.ToTable("RecipeDetails");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired();
    }
}