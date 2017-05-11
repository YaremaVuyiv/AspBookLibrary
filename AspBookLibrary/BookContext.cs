using System.Data.Entity;
using AspBookLibrary.Models;

namespace AspBookLibrary
{
    public class BookContext : DbContext
    {
        public DbSet<BookModel> Books { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            // Add any configuration or mapping stuff here
        }

        public void Seed(BookContext context)
        {
            // Normal seeding goes here
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
            Database.SetInitializer<BookContext> (new CreateInitializer ());
#endif
        }
    }
}