using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nutrition.Application.MenuPlans.Queries.Models;

namespace Nutrition.Infrastructure.Read.DatabaseObjectConfigurations;

internal class MenuPlanOverviewConfiguration : IEntityTypeConfiguration<MenuPlanOverview>
{
    public void Configure(EntityTypeBuilder<MenuPlanOverview> builder)
    {
        builder.ToTable("MenuPlanOverviews");
        builder.HasKey(mp => mp.Id);
        builder.Property(mp => mp.Id).ValueGeneratedNever();
        builder.Property(mp => mp.StartDate).IsRequired();
        builder.Property(mp => mp.EndDate).IsRequired();
        builder.Property(mp => mp.HasSnacking).IsRequired();
    }
}