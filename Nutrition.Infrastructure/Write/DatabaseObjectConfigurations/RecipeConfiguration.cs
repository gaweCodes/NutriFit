using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.Recipes;
using System.Reflection.Emit;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");
        builder.HasQueryFilter(r => !EF.Property<bool>(r, "_isDeleted"));
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(id => id.Value, value => new RecipeId(value))
            .ValueGeneratedNever();
        builder.Property("_name")
            .HasColumnName("Name")
            .IsRequired();
        builder.Property("_isDeleted")
            .HasColumnName("IsDeleted")
            .IsRequired();
    }
}