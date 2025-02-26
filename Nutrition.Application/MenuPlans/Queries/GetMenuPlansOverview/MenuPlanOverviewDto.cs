﻿namespace Nutrition.Application.MenuPlans.Queries.GetMenuPlansOverview;

public class MenuPlanOverviewDto(Guid id, DateOnly startDate, DateOnly endDate)
{
    public Guid Id { get; } = id;
    public DateOnly StartDate { get; } = startDate;
    public DateOnly EndDate { get; } = endDate;
}