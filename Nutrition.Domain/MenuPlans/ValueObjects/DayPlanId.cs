using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public record struct DayPlanId(Guid Value) : IEntityKeyValue;