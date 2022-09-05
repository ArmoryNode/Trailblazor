using System.Text.Json.Serialization;

#nullable disable

namespace Trailblazor.Domain.Common
{
    public abstract record BaseEntity
    {
        public string Id { get; init; }
        public Guid Identifier { get; init; } = Guid.NewGuid();
    }
}
