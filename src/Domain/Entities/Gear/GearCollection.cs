using Microsoft.EntityFrameworkCore;

namespace Trailblazor.Domain.Entities.Gear
{
    [Owned]
    public record GearCollection
    {
        public string Name { get; init; } = string.Empty;
        public int Order { get; init; }
        public GearItemDescriptor[] GearItems { get; init; } = Array.Empty<GearItemDescriptor>();
    }
}
