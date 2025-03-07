using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans.Entities;
using Nutrition.Domain.MenuPlans.ValueObjects;

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
    }
}