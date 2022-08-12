#nullable disable

using Trailblazor.Domain.Common;
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

        public override int SaveChanges()
        {
            MarkSoftDeleted();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            MarkSoftDeleted();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void MarkSoftDeleted()
        {
            // Iterate through tracked entries with type `ISoftDeletable`.
            foreach (var entry in ChangeTracker.Entries<ISoftDeletableEntity>())
            {
                // Only modify entities that are being marked for deletion.
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified; // Mark soft deletable entity for modification, instead of deletion.
                    entry.Entity.DeletedOn = DateTimeOffset.UtcNow; // Set the `DeletedOn` property to the current time.
                }
            }
        }
    }
}