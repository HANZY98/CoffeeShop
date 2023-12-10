using Microsoft.EntityFrameworkCore;
using Razor_temp.Models;

namespace Razor_temp.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
