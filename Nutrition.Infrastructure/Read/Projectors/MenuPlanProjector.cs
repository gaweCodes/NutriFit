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
        await dbContext.AddAsync(MenuPlanDetail.CreateNew(eventData.MenuPlanId, eventData.StartDate, eventData.EndDate, eventData.HasSnacking), cancellationToken);
        await dbContext.AddAsync(MenuPlanOverview.CreateNew(eventData.MenuPlanId, eventData.StartDate, eventData.EndDate, eventData.HasSnacking), cancellationToken);
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
            menuPlanOverview.HasSnacking = eventData.HasSnacking;
        }

        if (menuPlanDetail is not null)
        {
            menuPlanDetail.StartDate = eventData.StartDate;
            menuPlanDetail.EndDate = eventData.EndDate;
            menuPlanDetail.HasSnacking = eventData.HasSnacking;
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