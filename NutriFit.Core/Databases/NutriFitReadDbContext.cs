    using Microsoft.EntityFrameworkCore;

    namespace NutriFit.Core.Databases;

    public class NutriFitReadDbContext(DbContextOptions<NutriFitReadDbContext> options) : DbContext(options);