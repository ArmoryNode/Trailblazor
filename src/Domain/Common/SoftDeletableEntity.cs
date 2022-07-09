namespace Trailblazor.Domain.Common
{
    public abstract record SoftDeletableEntity
    {
        public DateTimeOffset? DeletedOn { get; set; }
        public bool Deleted => DeletedOn != null;
    }
}
