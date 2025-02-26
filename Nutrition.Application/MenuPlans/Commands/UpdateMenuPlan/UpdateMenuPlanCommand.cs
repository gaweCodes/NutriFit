using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.UpdateMenuPlan;

public class UpdateMenuPlanCommand(Guid id, DateOnly startDate, DateOnly endDate) : ICommand<Guid>
{
    public Guid Id { get; } = id;
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
}