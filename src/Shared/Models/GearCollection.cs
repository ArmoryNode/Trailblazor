using Microsoft.EntityFrameworkCore;

namespace Trailblazor.Shared.Models
{
    [Owned]
    public record GearCollection
    {
        public string Name { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<GearItem> GearItems { get; set; } = new();

        public WeightUnit PreferredWeightUnit { get; set; } = WeightUnit.Pounds;

        public Weight GearItemTotalWeight => new(GearItems.Sum(i => i.Weight.As(PreferredWeightUnit)), PreferredWeightUnit);
    }
}
