using Nutrition.Domain.MenuPlans.Events;
using Nutrition.Domain.MenuPlans.Rules;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class MenuPlan : Entity, IAggregateRoot
{
    public MenuPlanId Id { get; private set; } = null!;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsDeleted { get; private set; }
    public ICollection<DayPlan> Days { get; private set; } = [];

    private MenuPlan() { }
    private MenuPlan(DateOnly startDate, DateOnly endDate)
    {
        Id = new MenuPlanId(Guid.NewGuid());
        StartDate = startDate;
        EndDate = endDate;

        CheckRule(new StartDateBeforeEndDate(StartDate, EndDate));

        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
            Days.Add(new(date));

        AddDomainEvent(new MenuPlanCreatedDomainEvent(Id.Value, StartDate, EndDate));
    }
    public static MenuPlan CreateNew(DateOnly startDate, DateOnly endDate) => new(startDate, endDate);
    
    public void UpdateMenuPlan(DateOnly startDate, DateOnly endDate)
    {
        StartDate = startDate;
        EndDate = endDate;

        CheckRule(new StartDateBeforeEndDate(StartDate, EndDate));

        var dayList = Days.ToList();
        dayList.RemoveAll(x => x.Date < StartDate || x.Date > EndDate);       
        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
        {
            if (!dayList.Any(x => x.Date == date)) dayList.Add(new(date));
        }
        Days = dayList;

        AddDomainEvent(new MenuPlanUpdatedDomainEvent(Id.Value, StartDate, EndDate));
    }

    public void Delete()
    {
        IsDeleted = true;
        AddDomainEvent(new MenuPlanDeletedDomainEvent(Id.Value));
    }
}
