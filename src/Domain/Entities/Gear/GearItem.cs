using Trailblazor.Domain.Common;
using Trailblazor.Shared.Models;

namespace Trailblazor.Domain.Entities.Gear
{
    public record GearItem : AuditableEntity, ISoftDeletableEntity
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }

        public Weight Weight { get; init; }

        public bool IsWorn { get; init; }
        public bool IsConsumable { get; init; }
        public bool IsFavorite { get; init; }

        public string? Link { get; init; }

        public DateTimeOffset? DeletedOn { get; set; }
    }
}
