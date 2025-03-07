using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class MenuPlanConfiguration : IEntityTypeConfiguration<MenuPlan>
{
    public void Configure(EntityTypeBuilder<MenuPlan> builder)
    {
        builder.ToTable("MenuPlans");
        builder.HasQueryFilter(mp => !mp.IsDeleted);
        builder.HasKey(mp => mp.Id);
        builder.Property(mp => mp.Id)
            .HasConversion(id => id.Value, value => new MenuPlanId(value))
            .ValueGeneratedNever();
        builder.Property(mp => mp.StartDate)
            .IsRequired();
        builder.Property(mp => mp.EndDate)
            .IsRequired();
        builder.Property(mp => mp.IsDeleted)
            .IsRequired();

        builder.OwnsMany(mp => mp.Days, dayBuilder =>
        {
            dayBuilder.WithOwner().HasForeignKey("MenuPlanId");
            dayBuilder.ToTable("DayPlans");
            dayBuilder.HasKey(d => d.Id);
            dayBuilder.Property(d => d.Id).HasConversion(id => id.Value, value => new DayPlanId(value)).ValueGeneratedNever();
            dayBuilder.Property(d => d.Date).IsRequired();

            dayBuilder.OwnsMany(d => d.MealSlots, slotBuilder => 
            {
                slotBuilder.WithOwner().HasForeignKey("DayPlanId");
                slotBuilder.ToTable("MealSlots");
                slotBuilder.HasKey(ms => ms.Id);
                slotBuilder.Property(ms => ms.Id).HasConversion(id => id.Value, value => new MealSlotId(value)).ValueGeneratedNever();
                slotBuilder.Property(ms => ms.MealType).IsRequired();

                slotBuilder.OwnsMany(ms => ms.RecipeIds, recipeBuilder =>
                {
                    recipeBuilder.WithOwner().HasForeignKey("MealSlotId");
                    recipeBuilder.ToTable("MealSlotRecipes");
                    recipeBuilder.HasKey("MealSlotId", "Value");
                    recipeBuilder.Property(r => r.Value).HasColumnName("RecipeId").IsRequired().ValueGeneratedNever();
                });
            });
        });
    }
}