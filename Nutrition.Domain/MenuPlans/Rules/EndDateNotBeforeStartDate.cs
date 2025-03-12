using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Rules;

internal class EndDateNotBeforeStartDate(DateOnly startDate, DateOnly endDate) : IValidationRule
{
    public string Message => "Das Enddatum darf nicht vor dem Startdatum liegen.";

    public bool IsBroken() => endDate < startDate;
}
