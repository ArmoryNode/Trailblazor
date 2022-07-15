using Microsoft.EntityFrameworkCore;

namespace Trailblazor.Domain.Entities.Gear
{
    [Owned]
    public record GearItemDescriptor
    {
        public Guid GearItemId { get; init; }
        public int Order { get; init; }
        public int Quantity { get; init; }
    }
}
