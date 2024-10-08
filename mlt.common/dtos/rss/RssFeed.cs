﻿namespace mlt.common.dtos.rss;

public class RssFeed : Identifiable
{
    public string Name { get; init; } = null!;
    public string Url { get; init; } = null!;
    public DateTime LastUpdate { get; init; }
    public string Category { get; init; } = null!;
    public string DestinationFolder { get; init; } = null!;
    public bool ForceFirstSeasonFolder { get; set; }
    public string FileNameRegex { get; set; } = null!;
}