using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Application.MenuPlans.Queries.Models;

namespace Nutrition.Infrastructure.Read.DatabaseObjectConfigurations;

internal class MenuPlanDetailConfiguration : IEntityTypeConfiguration<MenuPlanDetail>
{
    public void Configure(EntityTypeBuilder<MenuPlanDetail> builder)
    {
        builder.ToTable("MenuPlanDetails");
        builder.HasKey(mp => mp.Id);
        builder.Property(mp => mp.Id).ValueGeneratedNever();
        builder.Property(mp => mp.StartDate).IsRequired();
        builder.Property(mp => mp.EndDate).IsRequired();
        builder.Property(mp => mp.HasSnacking).IsRequired();
    }
}