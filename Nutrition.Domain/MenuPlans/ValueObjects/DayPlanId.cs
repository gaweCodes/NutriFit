using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public readonly record struct DayPlanId(Guid Value) : IEntityKey;