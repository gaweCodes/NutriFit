    using Microsoft.EntityFrameworkCore;

    namespace NutriFit.ApiService.Databases;

    public class NutriFitReadDbContext(DbContextOptions<NutriFitReadDbContext> options) : DbContext(options);