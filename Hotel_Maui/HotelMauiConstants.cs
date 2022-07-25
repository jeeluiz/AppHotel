using Hotel.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Maui
{
    internal class HotelMauiConstants
    {
        public static string DbPath = Path.Combine(FileSystem.AppDataDirectory, "Hotel.db3");
        public static DbContextOptions<MeuDbContext> DbOptions = new DbContextOptionsBuilder<MeuDbContext>().UseSqlite($"Filename={DbPath}").Options;
    }
}
