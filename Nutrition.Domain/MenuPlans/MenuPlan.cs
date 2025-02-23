using Nutrition.Domain.MenuPlans.Events;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class MenuPlan : Entity, IAggregateRoot
{
    public MenuPlanId Id { get; }
    private DateOnly _startDate;
    private DateOnly _endDate;
    private bool _hasSnacking;
    private readonly List<DayPlan> _days = [];
    private bool _isDeleted;

    private MenuPlan() { }
    private MenuPlan(DateOnly startDate, DateOnly endDate, bool hasSnacking)
    {
        Id = new MenuPlanId(Guid.NewGuid());
        _startDate = startDate;
        _endDate = endDate;
        _hasSnacking = hasSnacking;

        for (var date = _startDate; date <= _endDate; date = date.AddDays(1))
            _days.Add(new DayPlan(date, _hasSnacking));

        AddDomainEvent(new MenuPlanCreatedDomainEvent(Id.Value, _startDate, _endDate, _hasSnacking));
    }
    public static MenuPlan CreateNew(DateOnly startDate, DateOnly endDate, bool hasSnacking) => new(startDate, endDate, hasSnacking);
    public void UpdateMenuPlan(DateOnly startDate, DateOnly endDate, bool hasSnacking)
    {
        _startDate = startDate;
        _endDate = endDate;
        _hasSnacking = hasSnacking;
        AddDomainEvent(new MenuPlanUpdatedDomainEvent(Id.Value, _startDate, _endDate, _hasSnacking));
    }

    public void Delete()
    {
        _isDeleted = true;
        AddDomainEvent(new MenuPlanDeletedDomainEvent(Id.Value));
    }
}
