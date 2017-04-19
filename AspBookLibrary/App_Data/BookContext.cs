using System.Data.Entity;
using AspBookLibrary.Models;

namespace AspBookLibrary.App_Data
{
    public class BookContext : DbContext
    {
        public DbSet<BookModels> Books { get; set; }

        public DbSet<GenreModels> Genres { get; set; }
    }
}