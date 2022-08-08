using Hotel.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public DbSet<Reserva> Hospedagems { get; set; }
        public DbSet<CategoriaQuarto> CategoriaQuartos { get; set; }
        public DbSet<CadastroHospede> CadastroHospede { get; set; }

        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options)
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();

            base.OnConfiguring(optionsBuilder);
        }
    }

}
