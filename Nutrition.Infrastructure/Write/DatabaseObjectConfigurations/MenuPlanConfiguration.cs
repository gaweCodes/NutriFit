using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Domain.MenuPlans;

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
        
        builder.HasMany(mp => mp.Days)
            .WithOne()
            .HasForeignKey("MenuPlanId").IsRequired();
    }
}