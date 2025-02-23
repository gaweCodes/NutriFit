using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Commands.DeleteMenuPlan;

public class DeleteMenuPlanCommand(Guid id) : ICommand
{
    public Guid Id { get; } = id;
}
