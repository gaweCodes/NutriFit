using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public readonly record struct MealSlotId(Guid Value) : IEntityKey;