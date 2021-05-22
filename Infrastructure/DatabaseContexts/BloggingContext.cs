using Infrastructure.Configurations;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.DatabaseContexts
{
    public class BloggingContext : DbContext
    {
        private readonly DatabaseOptions _databaseOptions;

        public BloggingContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions.Value;
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_databaseOptions.ConnectionString(), b => b.MigrationsAssembly("Web"));
        }
    }
}