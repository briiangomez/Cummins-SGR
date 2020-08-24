using SGR.Entities;
using Microsoft.EntityFrameworkCore;

namespace SGR.DAL.DataAccess
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Article> Article { get; set; }
        public DbSet<Category> Category { get; set; }

        public ApplicationDBContext() { }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    }
}
