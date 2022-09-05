namespace Trailblazor.Domain.Common
{
    public abstract record AuditableEntity : BaseEntity
    {
        public DateTimeOffset Created { get; init; } = DateTimeOffset.UtcNow;
        public string? CreatedBy { get; init; }
        public Guid CreatedById { get; init; }

        public DateTimeOffset? LastModified { get; init; } = DateTimeOffset.UtcNow;
        public string? LastModifiedBy { get; init; }
        public Guid LastModifiedById { get; init; }
    }
}
