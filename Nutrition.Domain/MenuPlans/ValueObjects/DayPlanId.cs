using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public class DayPlanId(Guid value) : TypedIdValueBase(value);