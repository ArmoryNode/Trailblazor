namespace Trailblazor.Server.Infrastructure
{
    public record struct CosmosDbSettings
    {
        public string DatabaseName { get; init; }
    }
}
