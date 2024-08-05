namespace mlt.common.domainEntities;

public class RssFeed
{
    public string? Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime LastUpdate { get; set; }

    public string Category { get; set; } = null!;

    public string DestinationFolder { get; set; } = null!;
}