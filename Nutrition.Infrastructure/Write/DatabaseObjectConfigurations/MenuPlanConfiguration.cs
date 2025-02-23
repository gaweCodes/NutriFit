using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans;

namespace Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

internal class MenuPlanConfiguration : IEntityTypeConfiguration<MenuPlan>
{
    public void Configure(EntityTypeBuilder<MenuPlan> builder)
    {
        builder.ToTable("MenuPlans");
        builder.HasQueryFilter(mp => !EF.Property<bool>(mp, "_isDeleted"));
        builder.HasKey(mp => mp.Id);
        builder.Property(mp => mp.Id)
            .HasConversion(id => id.Value, value => new MenuPlanId(value))
            .ValueGeneratedNever();
        builder.Property("_startDate")
            .HasColumnName("StartDate")
            .IsRequired();
        builder.Property("_endDate")
            .HasColumnName("EndDate")
            .IsRequired();
        builder.Property("_hasSnacking")
            .HasColumnName("HasSnacking")
            .IsRequired();
        builder.Property("_isDeleted")
            .HasColumnName("IsDeleted")
            .IsRequired();
    }
}