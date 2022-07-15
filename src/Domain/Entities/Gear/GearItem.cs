using Microsoft.EntityFrameworkCore;
using Trailblazor.Domain.Common;
using Trailblazor.Shared.Models;

namespace Trailblazor.Domain.Entities.Gear
{
    public record GearItem : AuditableEntity, ISoftDeletableEntity
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }

        public Weight Weight { get; init; }

        public bool Worn { get; init; }
        public bool Consumable { get; init; }
        public bool Favorite { get; init; }

        public DateTimeOffset? DeletedOn { get; init; }
    }
}
