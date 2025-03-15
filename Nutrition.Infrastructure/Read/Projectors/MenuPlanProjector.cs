using Nutrition.Application.MenuPlans.Queries.Models;
using Nutrition.Domain.MenuPlans.Events;
using Nutrition.Infrastructure.Read.Database;
using SharedKernel.Domain;

namespace Nutrition.Infrastructure.Read.Projectors;

internal class MenuPlanProjector(NutritionReadDbContext dbContext)
    : IDomainEventHandler<MenuPlanCreatedDomainEvent>,
    IDomainEventHandler<MenuPlanUpdatedDomainEvent>,
    IDomainEventHandler<MenuPlanDeletedDomainEvent>
{
    public async Task Handle(MenuPlanCreatedDomainEvent eventData, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(new MenuPlanDetail(eventData.MenuPlanId, eventData.StartDate, eventData.EndDate), cancellationToken);
        await dbContext.AddAsync(new MenuPlanOverview(eventData.MenuPlanId, eventData.StartDate, eventData.EndDate), cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(MenuPlanUpdatedDomainEvent eventData, CancellationToken cancellationToken)
    {
        var menuPlanDetail = await dbContext.FindAsync<MenuPlanDetail>(eventData.MenuPlanId, cancellationToken);
        var menuPlanOverview = await dbContext.FindAsync<MenuPlanOverview>(eventData.MenuPlanId, cancellationToken);

        if (menuPlanOverview is not null)
        {
            menuPlanOverview.StartDate = eventData.StartDate;
            menuPlanOverview.EndDate = eventData.EndDate;
        }

        if (menuPlanDetail is not null)
        {
            menuPlanDetail.StartDate = eventData.StartDate;
            menuPlanDetail.EndDate = eventData.EndDate;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Handle(MenuPlanDeletedDomainEvent eventData, CancellationToken cancellationToken)
    {
        var menuPlanDetail = await dbContext.FindAsync<MenuPlanDetail>(eventData.MenuPlanId, cancellationToken);
        var menuPlanOverview = await dbContext.FindAsync<MenuPlanOverview>(eventData.MenuPlanId, cancellationToken);

        if (menuPlanOverview is not null) dbContext.Remove(menuPlanOverview);
        if (menuPlanDetail is not null) dbContext.Remove(menuPlanDetail);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}