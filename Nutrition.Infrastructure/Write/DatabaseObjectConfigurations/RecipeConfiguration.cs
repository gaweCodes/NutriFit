using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.Recipes;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");
        builder.HasQueryFilter(r => !r.IsDeleted);
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(id => id.Value, value => new RecipeId(value))
            .ValueGeneratedNever();
        builder.Property(r => r.Name)
            .IsRequired();
        builder.Property(r => r.IsDeleted)
            .IsRequired();
    }
}