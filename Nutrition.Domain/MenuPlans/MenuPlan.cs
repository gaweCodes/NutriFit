using Nutrition.Domain.MenuPlans.Events;
using Nutrition.Domain.MenuPlans.Rules;
using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class MenuPlan : Entity, IAggregateRoot
{
    public MenuPlanId Id { get; } = new MenuPlanId(Guid.NewGuid());
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool HasSnacking { get; private set; }
    public bool IsDeleted { get; private set; }
    private readonly List<DayPlan> _days = [];

    private MenuPlan() { }
    private MenuPlan(DateOnly startDate, DateOnly endDate, bool hasSnacking)
    {
        StartDate = startDate;
        EndDate = endDate;
        HasSnacking = hasSnacking;

        CheckRule(new StartDateBeforeEndDate(this));

        for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
            _days.Add(new DayPlan(date, HasSnacking));

        AddDomainEvent(new MenuPlanCreatedDomainEvent(Id.Value, StartDate, EndDate, HasSnacking));
    }
    public static MenuPlan CreateNew(DateOnly startDate, DateOnly endDate, bool hasSnacking) => new(startDate, endDate, hasSnacking);
    public void UpdateMenuPlan(DateOnly startDate, DateOnly endDate, bool hasSnacking)
    {
        StartDate = startDate;
        EndDate = endDate;
        HasSnacking = hasSnacking;

        AddDomainEvent(new MenuPlanUpdatedDomainEvent(Id.Value, StartDate, EndDate, HasSnacking));
    }

    public void AddRecipeToMeal(DayPlanId dayPlanId, MealType mealType, Recipe recipe)
    {
        var dayPlan = _days.Single(x => x.Id == dayPlanId);
        dayPlan.AddRecipe(mealType, recipe);
    }

    public void Delete()
    {
        IsDeleted = true;
        AddDomainEvent(new MenuPlanDeletedDomainEvent(Id.Value));
    }
}
