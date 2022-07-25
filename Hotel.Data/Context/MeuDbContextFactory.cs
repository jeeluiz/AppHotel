using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Hotel.Data.Context
{
    internal sealed class MeuDbContextFactory : IDesignTimeDbContextFactory<MeuDbContext>
    {
        public MeuDbContext CreateDbContext(string[] args)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Hotel.db3");

            var optionsBuilder = new DbContextOptionsBuilder<MeuDbContext>();

            optionsBuilder.UseSqlite($"Filename={dbPath}");

            return new MeuDbContext(optionsBuilder.Options);
        }
    }
}
