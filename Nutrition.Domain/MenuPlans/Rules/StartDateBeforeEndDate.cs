using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Rules;

internal class StartDateBeforeEndDate(MenuPlan menuPlan) : IBusinessRule
{
    public string Message => "Das Startdatum muss vor dem Enddatum sein.";

    public bool IsBroken() => menuPlan.EndDate > menuPlan.StartDate;
}
