#nullable disable

using Trailblazor.Domain.Entities.Gear;

namespace Trailblazor.Infrastructure.Persistence
{
    public class TrailblazorDbContext : DbContext
    {
        public DbSet<GearList> GearLists { get; set; }
        public DbSet<GearItem> GearItems { get; set; }

        public TrailblazorDbContext(DbContextOptions<TrailblazorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GearList>(builder =>
            {
                builder.ToContainer("Gear");
                builder.HasDiscriminator<string>(nameof(GearList));
            });

            modelBuilder.Entity<GearItem>(builder =>
            {
                builder.ToContainer("Gear");
                builder.HasDiscriminator<string>(nameof(GearItem));
            });
        }
    }
}