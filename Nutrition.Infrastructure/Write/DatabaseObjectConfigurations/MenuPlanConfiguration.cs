using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class MenuPlanConfiguration : IEntityTypeConfiguration<MenuPlan>
{
    public void Configure(EntityTypeBuilder<MenuPlan> builder)
    {
        builder.ToTable("MenuPlans");
        builder.HasQueryFilter(mp => EF.Property<bool>(mp, "_isDeleted") == false);
        builder.HasKey(mp => mp.Id);
        builder.Property(mp => mp.Id)
            .HasConversion(id => id.Value, value => new MenuPlanId(value))
            .ValueGeneratedNever();
        builder.Property<DateOnly>("_startDate")
            .HasColumnName("StartDate")
            .IsRequired();
        builder.Property<DateOnly>("_endDate")
            .HasColumnName("EndDate")
            .IsRequired();
        builder.Property<bool>("_isDeleted")
            .HasColumnName("IsDeleted")
            .IsRequired();
        builder.OwnsMany<DayPlan>("_days", dp => 
        {
            dp.WithOwner().HasForeignKey("MenuPlanId");
            dp.ToTable("DayPlans");
            dp.HasKey("Id");
            dp.Property<DayPlanId>("Id").HasConversion(id => id.Value, value => new DayPlanId(value)).ValueGeneratedNever();
            dp.Property<MenuPlanId>("MenuPlanId");
            dp.Property<DateOnly>("Date").IsRequired();
            dp.OwnsMany<MealSlot>("_mealSlots", ms =>
            {
                ms.WithOwner().HasForeignKey("DayPlanId");
                ms.ToTable("MealSlotss");
                ms.HasKey("Id");
                ms.Property<MealSlotId>("Id").HasConversion(id => id.Value, value => new MealSlotId(value)).ValueGeneratedNever();
                ms.Property<DayPlanId>("DayPlanId");
                ms.Property<MealType>("MealType").IsRequired();
            });
        });
    }
}