using Nutrition.Domain.MenuPlans.Events;
using Nutrition.Domain.MenuPlans.Rules;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class MenuPlan : Entity, IAggregateRoot
{
    public MenuPlanId Id { get; private set; } = null!;
    private DateOnly _startDate;
    private DateOnly _endDate;
    private bool _isDeleted;
    private readonly List<DayPlan> _days = [];

    private MenuPlan() { }
    private MenuPlan(DateOnly startDate, DateOnly endDate)
    {
        Id = new MenuPlanId(Guid.NewGuid());
        _startDate = startDate;
        _endDate = endDate;

        CheckRule(new StartDateBeforeEndDate(_startDate, _endDate));

        for (var date = _startDate; date <= _endDate; date = date.AddDays(1))
            _days.Add(new(date));

        AddDomainEvent(new MenuPlanCreatedDomainEvent(Id.Value, _startDate, _endDate));
    }
    public static MenuPlan CreateNew(DateOnly startDate, DateOnly endDate) => new(startDate, endDate);
    
    public void UpdateMenuPlan(DateOnly startDate, DateOnly endDate)
    {
        _startDate = startDate;
        _endDate = endDate;

        CheckRule(new StartDateBeforeEndDate(_startDate, _endDate));

        _days.RemoveAll(x => x.Date < _startDate || x.Date > _endDate);       
        for (var date = _startDate; date <= _endDate; date = date.AddDays(1))
        {
            if (!_days.Any(x => x.Date == date)) _days.Add(new(date));
        }

        AddDomainEvent(new MenuPlanUpdatedDomainEvent(Id.Value, _startDate, _endDate));
    }

    public void Delete()
    {
        _isDeleted = true;
        AddDomainEvent(new MenuPlanDeletedDomainEvent(Id.Value));
    }
}
