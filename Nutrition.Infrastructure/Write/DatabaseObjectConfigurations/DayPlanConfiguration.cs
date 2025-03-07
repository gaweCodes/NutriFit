using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class DayPlanConfiguration : IEntityTypeConfiguration<DayPlan>
{
    public void Configure(EntityTypeBuilder<DayPlan> builder)
    {
        builder.ToTable("DayPlans");
        builder.HasKey(dp => dp.Id);
        builder.Property(dp => dp.Id)
            .HasConversion(id => id.Value, value => new DayPlanId(value))
            .ValueGeneratedNever();
        builder.Property(Dp => Dp.Date)
            .IsRequired();

        builder.HasMany(dp => dp.MealSlots)
            .WithOne()
            .HasForeignKey("DayPlanId").IsRequired();
    }
}