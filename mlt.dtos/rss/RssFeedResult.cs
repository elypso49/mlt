namespace mlt.dtos.rss;

public class RssFeedResult : Identifiable
{
    public string RssFeedId { get; init; } = null!;
    public string? Title { get; init; } = null!;
    public string? Link { get; init; } = null!;
    public string? Description { get; init; } = null!;
    public DateTime PublishDate { get; init; }

    // custom fields
    public DateTime CreatedDate { get; init; } = DateTime.Now;
    public StateValue State { get; set; } = 0;
    public DateTime? UpdatedDate { get; init; }

    // tv fields
    public string? TvShowId { get; init; }
    public string? TvExternalId { get; init; }
    public string? TvShowName { get; init; }
    public string? TvEpisodeId { get; init; }
    public string? TvRawTitle { get; init; }
    public string? TvInfoHash { get; init; }

    // nyaa fields
    public int? NyaaSeeders { get; init; }
    public int? NyaaLeechers { get; init; }
    public int? NyaaDownloads { get; init; }
    public string? NyaaInfoHash { get; init; }
    public string? NyaaCategoryId { get; init; }
    public string? NyaaCategory { get; init; }
    public string? NyaaSize { get; init; }
    public int? NyaaComments { get; init; }
    public string? NyaaTrusted { get; init; }
    public string? NyaaRemake { get; init; }

    // Process fields
    // public RealDebridTorrentInfo RealDebridTorrentInfo { get; set; } = new();
    // public List<string> DebridedUrl { get; set; }
}