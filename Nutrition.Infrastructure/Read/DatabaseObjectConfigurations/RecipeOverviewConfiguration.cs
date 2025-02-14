using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Application.Recipes.Queries.Models;

namespace Nutrition.Infrastructure.Read.DatabaseObjectConfigurations;

internal class RecipeOverviewConfiguration : IEntityTypeConfiguration<RecipeOverview>
{
    public void Configure(EntityTypeBuilder<RecipeOverview> builder)
    {
        builder.ToTable("RecipeOverviews");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired();
    }
}