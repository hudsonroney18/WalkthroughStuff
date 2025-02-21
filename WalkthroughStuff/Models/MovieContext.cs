using Microsoft.EntityFrameworkCore;

namespace WalkthroughStuff.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base (options) { 

        }   
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
            
                new Category { CategoryId = 1, CategoryName = "Sci-Fi"},
                new Category { CategoryId = 2, CategoryName = "Romance"},
                new Category { CategoryId = 3, CategoryName = "Horror"},
                new Category { CategoryId = 4, CategoryName = "Adventure"},
                new Category { CategoryId = 5, CategoryName = "Drama"}
                );
        }
    }
}
