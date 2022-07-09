namespace Trailblazor.Domain.Common
{
    public abstract record BaseEntity
    {
        public Guid Id { get; init; }
    }
}
