namespace Trailblazor.Infrastructure.Configurations
{
    public record struct CosmosDbSettings
    {
        public string DatabaseName { get; init; }
    }
}
