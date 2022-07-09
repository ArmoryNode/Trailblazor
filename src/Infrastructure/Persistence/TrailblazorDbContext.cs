#nullable disable

namespace Trailblazor.Infrastructure.Persistence
{
    public class TrailblazorDbContext : DbContext
    {
        public TrailblazorDbContext(DbContextOptions<TrailblazorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}