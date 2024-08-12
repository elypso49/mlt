using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Extensions.Options;
using mlt.common.options;
using mlt.rss.datas.models;
using mlt.rss.dtos;
using MongoDB.Driver;

namespace mlt.rss.datas;

internal class RssFeedProcessorRepository : IRssFeedProcessorRepository
{
    private readonly IRssFeedResultRepository _rssFeedResultRepository;
    private readonly IMongoCollection<RssFeedModel> _rssFeeds;

    public RssFeedProcessorRepository(IOptions<MongoDbOptions> settings, IRssFeedResultRepository rssFeedResultRepository)
    {
        _rssFeedResultRepository = rssFeedResultRepository;
        var client = new MongoClient(settings.Value.ConnectionString);
        _rssFeeds = client.GetDatabase(settings.Value.RssLibraryDatabaseName).GetCollection<RssFeedModel>("RssFeeds");
    }

    public async Task ProcessFeed(string rssFeedId)
    {
        var rssFeed = _rssFeeds.Find(feed => feed.Id == rssFeedId).FirstOrDefault();

        if (rssFeed == null)
            throw new Exception("RSS Feed not found.");
        
        using var reader = XmlReader.Create(rssFeed.Url);

        var doc = XDocument.Load(reader);
        XNamespace tv = "https://showrss.info";
        XNamespace nyaa = "https://nyaa.si/xmlns/nyaa";

        foreach (var item in doc.Descendants("item"))
        {
            var title = item.Element("title")?.Value;
            var link = item.Element("link")?.Value;
            var description = item.Element("description")?.Value;
            var publishDate = DateTime.Parse(item.Element("pubDate")?.Value ?? DateTime.MinValue.ToString(CultureInfo.InvariantCulture));
            var tvShowId = item.Element(tv + "show_id")?.Value;
            var tvExternalId = item.Element(tv + "external_id")?.Value;
            var tvShowName = item.Element(tv + "show_name")?.Value;
            var tvEpisodeId = item.Element(tv + "episode_id")?.Value;
            var tvRawTitle = item.Element(tv + "raw_title")?.Value;
            var tvInfoHash = item.Element(tv + "info_hash")?.Value;
            var nyaaSeeders = int.TryParse(item.Element(nyaa + "seeders")?.Value, out var seeders) ? seeders : 0;
            var nyaaLeechers = int.TryParse(item.Element(nyaa + "leechers")?.Value, out var leechers) ? leechers : 0;
            var nyaaDownloads = int.TryParse(item.Element(nyaa + "downloads")?.Value, out var downloads) ? downloads : 0;
            var nyaaInfoHash = item.Element(nyaa + "infoHash")?.Value;
            var nyaaCategoryId = item.Element(nyaa + "categoryId")?.Value;
            var nyaaCategory = item.Element(nyaa + "category")?.Value;
            var nyaaSize = item.Element(nyaa + "size")?.Value;
            var nyaaComments = int.TryParse(item.Element(nyaa + "comments")?.Value, out var comments) ? comments : 0;
            var nyaaTrusted = item.Element(nyaa + "trusted")?.Value;
            var nyaaRemake = item.Element(nyaa + "remake")?.Value;
            
            if (!(await _rssFeedResultRepository.GetAll()).Any(r => (!string.IsNullOrEmpty(r.TvInfoHash) && r.TvInfoHash == tvInfoHash)
                                                               || (!string.IsNullOrEmpty(r.NyaaInfoHash) && r.NyaaInfoHash == nyaaInfoHash)))
            {
                var result = new RssFeedResult
                             {
                                 RssFeedId = rssFeedId,
                                 Title = title,
                                 Link = link,
                                 Description = description,
                                 PublishDate = publishDate,
                                 TvShowId = tvShowId,
                                 TvExternalId = tvExternalId,
                                 TvShowName = tvShowName,
                                 TvEpisodeId = tvEpisodeId,
                                 TvRawTitle = tvRawTitle,
                                 TvInfoHash = tvInfoHash,
                                 NyaaSeeders = nyaaSeeders,
                                 NyaaLeechers = nyaaLeechers,
                                 NyaaDownloads = nyaaDownloads,
                                 NyaaInfoHash = nyaaInfoHash,
                                 NyaaCategoryId = nyaaCategoryId,
                                 NyaaCategory = nyaaCategory,
                                 NyaaSize = nyaaSize,
                                 NyaaComments = nyaaComments,
                                 NyaaTrusted = nyaaTrusted,
                                 NyaaRemake = nyaaRemake
                             };

                await _rssFeedResultRepository.Add(result);
            }
        }
    }
}