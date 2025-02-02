    using Microsoft.EntityFrameworkCore;

    namespace NutriFit.Core.Databases;

    public class NutriFitWriteDbContext(DbContextOptions<NutriFitWriteDbContext> options) : DbContext(options);