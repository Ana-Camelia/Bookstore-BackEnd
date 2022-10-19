using Bookstore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bookstore.DataAccess.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
