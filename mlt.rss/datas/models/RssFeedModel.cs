using mlt.common;

namespace mlt.rss.datas.models;

public class RssFeedModel : BsonIdentifiable
{
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
    public string Category { get; set; } = null!;
    public string DestinationFolder { get; set; } = null!;
}