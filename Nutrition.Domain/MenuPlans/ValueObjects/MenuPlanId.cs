using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public readonly record struct MenuPlanId(Guid Value) : IEntityKey;