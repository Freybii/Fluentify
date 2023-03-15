using Microsoft.EntityFrameworkCore;
using Fluentify.Models.Database.Tables;
namespace Fluentify.Database
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public StoreDbContext() { }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            options.UseSqlServer();
        }

    }
}