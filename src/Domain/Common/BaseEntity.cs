using System.Text.Json.Serialization;

namespace Trailblazor.Domain.Common
{
    public abstract record BaseEntity
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
    }
}
