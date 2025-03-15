using Nutrition.Domain.MenuPlans.Checkers;
using Nutrition.Domain.MenuPlans.Events;
using Nutrition.Domain.MenuPlans.Rules;
using Nutrition.Domain.MenuPlans.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Entities;

public class MenuPlan : Entity, IAggregateRoot
{
    public MenuPlanId Id { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsDeleted { get; private set; }
    public ICollection<DayPlan> Days { get; private set; } = [];

    private MenuPlan() { }
    private MenuPlan(DateOnly startDate, DateOnly endDate, IUniqueMenuPlanDateRangeChecker uniqueMenuPlanDateRangeChecker)
    {
        Id = new MenuPlanId(Guid.NewGuid());
        StartDate = startDate;
        EndDate = endDate;

        CheckRule(new EndDateNotBeforeStartDate(StartDate, EndDate));
        if (uniqueMenuPlanDateRangeChecker.Check(this) == false)
            throw new BusinessRuleException("Die Menüpläne müssen einen eindeutigen Zeitraum abdecken. Es darf keine Überschneidungen geben.");

        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
            Days.Add(new(date));

        AddUncommittedEvent(new MenuPlanCreatedDomainEvent(Id.Value, StartDate, EndDate));
    }
    public static MenuPlan CreateNew(DateOnly startDate, DateOnly endDate, IUniqueMenuPlanDateRangeChecker uniqueMenuPlanDateRangeChecker) => 
        new(startDate, endDate, uniqueMenuPlanDateRangeChecker);
    
    public void UpdateMenuPlan(DateOnly startDate, DateOnly endDate, IUniqueMenuPlanDateRangeChecker uniqueMenuPlanDateRangeChecker)
    {
        StartDate = startDate;
        EndDate = endDate;

        CheckRule(new EndDateNotBeforeStartDate(StartDate, EndDate));
        if (uniqueMenuPlanDateRangeChecker.Check(this) == false)
            throw new BusinessRuleException("Die Menüpläne müssen einen eindeutigen Zeitraum abdecken. Es darf keine Überschneidungen geben.");

        var dayList = Days.ToList();
        dayList.RemoveAll(x => x.Date < StartDate || x.Date > EndDate);       
        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
        {
            if (!dayList.Any(x => x.Date == date)) dayList.Add(new(date));
        }
        Days = dayList;

        AddUncommittedEvent(new MenuPlanUpdatedDomainEvent(Id.Value, StartDate, EndDate));
    }

    public void Delete()
    {
        IsDeleted = true;
        AddUncommittedEvent(new MenuPlanDeletedDomainEvent(Id.Value));
    }
}
