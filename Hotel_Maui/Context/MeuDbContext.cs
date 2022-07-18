using Hotel_Maui.Model;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Maui.Context
{
    public class MeuDbContext : DbContext
    {
        public DbSet<Reserva> Hospedagems { get; set; }
        public DbSet<CategoriaQuarto> CategoriaQuartos { get; set; }
        public DbSet<CadastroHospede> CadastroHospede { get; set; }

        public MeuDbContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Hotel.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }


}
