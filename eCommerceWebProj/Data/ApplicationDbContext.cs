using eCommerceWebProj.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceWebProj.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
    }
}
