namespace Trailblazor.Domain.Common
{
    public interface ISoftDeletableEntity
    {
        public DateTimeOffset? DeletedOn { get; init; }
        public bool Deleted => DeletedOn != null;
    }
}
