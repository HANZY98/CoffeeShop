using Microsoft.EntityFrameworkCore;
using mvcapplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace mvcapplication.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        
        public DbSet<ShoppingCart> ShopCart { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Users>  Users { get; set; }
        public DbSet<ApplicationUser> AppUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         

            modelBuilder.Entity<Category>().HasData(
    new Category { Category_Id = 1, Name = "Action", DisplayOrder = 1, JobTitle = "GM" },
    new Category { Category_Id = 2, Name = "Adventure", DisplayOrder = 2, JobTitle = "RM" },
    new Category { Category_Id = 3, Name = "Comedy", DisplayOrder = 3, JobTitle = "GM" },
    new Category { Category_Id = 4, Name = "Drama", DisplayOrder = 4, JobTitle = "RM" }
);

            modelBuilder.Entity<Product>().HasData(
                new Product { Product_Id = 1, Title = "MR Bean 1", Description = "This film is classic", ISBN = "", Author = "Bean", Price = 20, Price50 = 100, Price100 = 200, Category_Id = 1, ImageURL = "" },
                new Product { Product_Id = 2, Title = "MR Bean 2", Description = "Another classic", ISBN = "", Author = "Bean", Price = 20, Price50 = 100, Price100 = 200, Category_Id = 2, ImageURL = "" },
                new Product { Product_Id = 3, Title = "MR Bean 3", Description = "Yet another classic", ISBN = "", Author = "Bean", Price = 20, Price50 = 100, Price100 = 200, Category_Id = 3, ImageURL = "" },
                new Product { Product_Id = 4, Title = "Another Movie", Description = "Not related to Mr. Bean", ISBN = "", Author = "Unknown", Price = 30, Price50 = 150, Price100 = 300, Category_Id =4, ImageURL = "" }
            );




            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, FirstName = "Luke", SecondName ="Hands", AboutMe ="I love coding", JobTitle="Engineer", Salary="100,000"},
                new Users { Id = 2, FirstName = "Jason", SecondName = "Hands", AboutMe = "I love coding", JobTitle = "Engineer", Salary = "100,000" },
                new Users { Id = 3, FirstName = "Dean", SecondName = "Hands", AboutMe = "I love coding", JobTitle = "Engineer", Salary = "100,000" },
                new Users { Id = 4, FirstName = "Duke", SecondName = "Hands", AboutMe = "I love coding", JobTitle = "Engineer", Salary = "100,000" },
                new Users { Id = 5, FirstName = "Grham", SecondName = "Hands", AboutMe = "I love coding", JobTitle = "Engineer", Salary = "100,000" }


                );
        }

        
            
        
    }
}
