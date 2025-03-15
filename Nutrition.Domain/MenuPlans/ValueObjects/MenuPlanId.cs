using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public record struct MenuPlanId(Guid Value) : IAggregateId;