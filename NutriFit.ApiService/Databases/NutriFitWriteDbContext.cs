
    using Microsoft.EntityFrameworkCore;

    namespace NutriFit.ApiService.Databases;

    public class NutriFitWriteDbContext(DbContextOptions<NutriFitWriteDbContext> options) : DbContext(options);