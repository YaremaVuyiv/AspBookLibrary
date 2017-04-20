using System.Data.Entity;
using System.Data.Entity.Migrations;
using AspBookLibrary.Models;

namespace AspBookLibrary.App_Data
{
    public class BookContext : DbContext
    {
        public DbSet<BookModel> Books { get; set; }

        public DbSet<GenreModels> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            // Add any configuration or mapping stuff here
        }

        public void Seed(BookContext context)
        {
            // Normal seeding goes here
            context.Genres.AddOrUpdate(genre => genre.Name,
            new[]{
                new GenreModels { Description = "Fiction", Name = "Fiction" },
                new GenreModels { Description = "Comedy", Name = "Comedy" },
                new GenreModels { Description = "Drama", Name = "Drama" },
                new GenreModels { Description = "Horror", Name = "Horror" },
                new GenreModels { Description = "Non-fiction", Name = "Non-fiction" },
                new GenreModels { Description = "Realistic fiction", Name = "Realistic fiction" },
                new GenreModels { Description = "Romance novel", Name = "Romance novel" },
                new GenreModels { Description = "Satire", Name = "Satire" },
                new GenreModels { Description = "Tragedy", Name = "Tragedy" },
                new GenreModels { Description = "Tragicomedy", Name = "Tragicomedy" },
                new GenreModels { Description = "Fantasy", Name = "Fantasy" },
                new GenreModels { Description = "Mythology", Name = "Mythology" }
            });

            context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<BookContext>
        {
            protected override void Seed(BookContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<BookContext>
        {
            protected override void Seed(BookContext context)
            {
                context.Seed(context);

                base.Seed(context);
            }
        }

        static BookContext()
        {
#if DEBUG
            Database.SetInitializer<BookContext>(new DropCreateIfChangeInitializer());
#else
            Database.SetInitializer<MyDbContext> (new CreateInitializer ());
#endif
        }
    }
}