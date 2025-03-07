using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public class MealSlotId(Guid value) : TypedIdValueBase(value);