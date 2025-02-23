using SharedKernel.Application;

namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlan;

public class GetMenuPlanQuery(Guid id) : IQuery<MenuPlanDto?>
{
    public Guid Id { get; } = id;
}