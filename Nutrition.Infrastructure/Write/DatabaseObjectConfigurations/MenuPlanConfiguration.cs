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
        builder.OwnsMany<DayPlan>("_days", y => 
        {
            y.WithOwner().HasForeignKey("MenuPlanId");
            y.ToTable("DayPlans");
            y.HasKey("Id");
            y.Property<DayPlanId>("Id").HasConversion(id => id.Value, value => new DayPlanId(value)).ValueGeneratedNever();
            y.Property<MenuPlanId>("MenuPlanId");
            y.Property<DateOnly>("Date").IsRequired();
        });
    }
}