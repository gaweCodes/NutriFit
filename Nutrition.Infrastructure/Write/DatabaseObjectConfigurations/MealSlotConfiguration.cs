using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans;
using Nutrition.Domain.Recipes;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class MealSlotConfiguration : IEntityTypeConfiguration<MealSlot>
{
    public void Configure(EntityTypeBuilder<MealSlot> builder)
    {
        builder.ToTable("MealSlots");
        builder.HasKey(ms => ms.Id);
        builder.Property(ms => ms.Id)
            .HasConversion(id => id.Value, value => new MealSlotId(value))
            .ValueGeneratedNever();
        builder.Property(ms => ms.MealType).IsRequired();

        builder.HasMany(ms => ms.Recipes)
            .WithMany()
                        .UsingEntity<Dictionary<string, object>>(
            "MealSlotRecipes", 
            j => j.HasOne<Recipe>().WithMany().HasForeignKey("RecipeId").IsRequired(), 
            j => j.HasOne<MealSlot>().WithMany().HasForeignKey("MealSlotId").IsRequired(),
            j =>
            {
                j.ToTable("MealSlotRecipes");
                j.HasKey("MealSlotId", "RecipeId");
            });
    }
}