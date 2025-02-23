using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.CreateMenuPlan;

public class CreateMenuPlanCommand(DateOnly startDate, DateOnly endDate, bool hasSnacking) : ICommand<Guid>
{
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
    public bool HasSnacking { get; } = hasSnacking;
}