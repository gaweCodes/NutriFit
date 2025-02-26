using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Rules;

internal class StartDateBeforeEndDate(DateOnly startDate, DateOnly endDate) : IBusinessRule
{
    public string Message => "Das Startdatum muss vor dem Enddatum sein.";

    public bool IsBroken() => endDate < startDate;
}
