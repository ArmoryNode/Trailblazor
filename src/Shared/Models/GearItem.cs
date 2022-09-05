using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Trailblazor.Shared.Models
{
    [Owned]
    public record GearItem
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Weight Weight { get; set; } = new();

        public bool IsWorn { get; set; } = false;
        public bool IsConsumable { get; set; } = false;
        public bool IsFavorite { get; set; } = false;

        public string Link { get; set; } = string.Empty;

        public int Order { get; init; } = 1;

        public int Quantity { get; init; } = 1;
    }
}
