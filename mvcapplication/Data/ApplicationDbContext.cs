using Microsoft.EntityFrameworkCore;
using mvcapplication.Models;

namespace mvcapplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Category_Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Category_Id = 2, Name = "Action", DisplayOrder = 2 },
                new Category { Category_Id = 3, Name = "Action", DisplayOrder = 3 }
                );
        }
    }
}
