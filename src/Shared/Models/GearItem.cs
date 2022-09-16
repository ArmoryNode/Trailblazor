using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trailblazor.Shared.Models
{
    [Owned]
    public record GearItem
    {
        [NotMapped]
        public readonly Guid UUID = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Weight Weight { get; set; } = new();

        public bool IsWorn { get; set; } = false;
        public bool IsConsumable { get; set; } = false;
        public bool IsFavorite { get; set; } = false;

        public string Link { get; set; } = string.Empty;

        public int Order { get; set; } = 1;

        public int Quantity { get; set; } = 1;
    }
}
