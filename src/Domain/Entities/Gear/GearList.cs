using Trailblazor.Domain.Common;

namespace Trailblazor.Domain.Entities.Gear
{
    public record GearList : AuditableEntity, ISoftDeletableEntity
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }

        public int Order { get; init; }
        public bool Favorite { get; init; }

        public GearCollection[] GearCollections { get; init; } = Array.Empty<GearCollection>();

        public DateTimeOffset? DeletedOn { get; init; }
    }
}
