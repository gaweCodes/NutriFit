﻿using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.ValueObjects;

public record struct MealSlotId(Guid Value) : IEntityKeyValue;