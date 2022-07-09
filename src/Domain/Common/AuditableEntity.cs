namespace Trailblazor.Domain.Common
{
    public abstract record AuditableEntity : BaseEntity
    {
        public DateTimeOffset Created { get; init; }
        public string? CreatedBy { get; init; }
        public DateTimeOffset? LastModified { get; init; }
        public string? LastModifiedBy { get; init; }
    }
}
