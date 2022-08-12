namespace Trailblazor.Domain.Common
{
    public interface ISoftDeletableEntity
    {
        public DateTimeOffset? DeletedOn { get; set; }
        public bool Deleted => DeletedOn != null;
    }
}
