﻿using mlt.common;

namespace mlt.rss.repositories.models;

internal class RssFeedModel : BsonIdentifiable
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
    public string Category { get; set; } = null!;
    public string DestinationFolder { get; set; } = null!;
    public bool ForceFirstSeasonFolder { get; set; }
    public string FileNameRegex { get; set; } = string.Empty;
}