#nullable disable

using Trailblazor.Domain.Common;
using Trailblazor.Domain.Entities.Gear;

namespace Trailblazor.Infrastructure.Persistence
{
    public class TrailblazorDbContext : DbContext
    {
        public DbSet<GearList> GearLists { get; set; }

        public TrailblazorDbContext(string connectionString) : base(new DbContextOptionsBuilder<TrailblazorDbContext>().UseCosmos(connectionString, "TrailblazorDb").Options)
        {
        }

        public TrailblazorDbContext(DbContextOptions<TrailblazorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GearList>(builder =>
            {
                builder.HasPartitionKey(nameof(GearList.CreatedById))
                       .ToContainer("Gear")
                       .HasDiscriminator<string>("Discriminator")
                       .HasValue(nameof(GearList));

                builder.Property(x => x.Id).ToJsonProperty("id");

                builder.HasKey(gl => gl.Identifier);
            });
        }

        public override int SaveChanges()
        {
            MarkSoftDeleted();
            UpdateSearchText();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            MarkSoftDeleted();
            UpdateSearchText();
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

        private void UpdateSearchText()
        {
            // Iterate through tracked entries with type `ISearchable`.
            foreach (var entry in ChangeTracker.Entries<ISearchable>())
            {
                // Only modify entity if it has been added or modified.
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    // Call method to update the search text.
                    entry.Entity.UpdateSearchText();
                }
            }
        }
    }
}