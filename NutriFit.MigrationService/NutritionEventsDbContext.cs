using Microsoft.EntityFrameworkCore;

namespace NutriFit.MigrationService;

internal class NutritionEventsDbContext(DbContextOptions<NutritionEventsDbContext> options) : DbContext(options);