using mlt.common.enums;

namespace mlt.common.domainEntities;

public class RssFeedResult
{
    // default
    public string? Id { get; set; }

    public string RssFeedId { get; set; } = null!;

    public string? Title { get; set; } = null!;

    public string? Link { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public DateTime PublishDate { get; set; }

    // custom fields
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public StateValue State { get; set; } = StateValue.UnRead;

    public DateTime? UpdatedDate { get; set; }

    // tv fields
    public string? TvShowId { get; set; }

    public string? TvExternalId { get; set; }

    public string? TvShowName { get; set; }

    public string? TvEpisodeId { get; set; }

    public string? TvRawTitle { get; set; }

    public string? TvInfoHash { get; set; }

    // nyaa fields
    public int? NyaaSeeders { get; set; }

    public int? NyaaLeechers { get; set; }

    public int? NyaaDownloads { get; set; }

    public string? NyaaInfoHash { get; set; }

    public string? NyaaCategoryId { get; set; }

    public string? NyaaCategory { get; set; }

    public string? NyaaSize { get; set; }

    public int? NyaaComments { get; set; }

    public string? NyaaTrusted { get; set; }

    public string? NyaaRemake { get; set; }
}