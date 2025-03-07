using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public class MenuPlanId(Guid value) : TypedIdValueBase(value);