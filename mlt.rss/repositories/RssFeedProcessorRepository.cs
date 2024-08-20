namespace mlt.rss.repositories;

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

    public async Task<RssSyncResult?> ProcessFeed(string rssFeedId)
    {
        var result = new RssSyncResult();
        var rssFeed = _rssFeeds.Find(feed => feed.Id == rssFeedId).FirstOrDefault();

        if (rssFeed == null)
            return null;

        result.RssFeedId = rssFeed.Id!;
        result.Name = rssFeed.Name;
        result.Added = 0;

        var existingFeedResults = (await _rssFeedResultRepository.GetAll()).ToList();

        using var reader = XmlReader.Create(rssFeed.Url);

        var doc = XDocument.Load(reader);
        XNamespace tv = "https://showrss.info";
        XNamespace nyaa = "https://nyaa.si/xmlns/nyaa";

        foreach (var item in doc.Descendants("item").Where(FilterExistingElements(existingFeedResults, tv, nyaa)))
        {
            await _rssFeedResultRepository.Add(new RssFeedResult
            {
                RssFeedId = rssFeedId,
                Title = item.Element("title")?.Value,
                Link = item.Element("link")?.Value,
                Description = item.Element("description")?.Value,
                PublishDate = DateTime.Parse(item.Element("pubDate")?.Value ?? DateTime.MinValue.ToString(CultureInfo.InvariantCulture)),
                TvShowId = item.Element(tv + "show_id")?.Value,
                TvExternalId = item.Element(tv + "external_id")?.Value,
                TvShowName = item.Element(tv + "show_name")?.Value,
                TvEpisodeId = item.Element(tv + "episode_id")?.Value,
                TvRawTitle = item.Element(tv + "raw_title")?.Value,
                TvInfoHash = item.Element(tv + "info_hash")?.Value,
                NyaaSeeders = int.TryParse(item.Element(nyaa + "seeders")?.Value, out var seeders) ? seeders : 0,
                NyaaLeechers = int.TryParse(item.Element(nyaa + "leechers")?.Value, out var leechers) ? leechers : 0,
                NyaaDownloads = int.TryParse(item.Element(nyaa + "downloads")?.Value, out var downloads) ? downloads : 0,
                NyaaInfoHash = item.Element(nyaa + "infoHash")?.Value,
                NyaaCategoryId = item.Element(nyaa + "categoryId")?.Value,
                NyaaCategory = item.Element(nyaa + "category")?.Value,
                NyaaSize = item.Element(nyaa + "size")?.Value,
                NyaaComments = int.TryParse(item.Element(nyaa + "comments")?.Value, out var comments) ? comments : 0,
                NyaaTrusted = item.Element(nyaa + "trusted")?.Value,
                NyaaRemake = item.Element(nyaa + "remake")?.Value
            });

            result.Added++;
        }

        return result;
    }

    private static Func<XElement, bool> FilterExistingElements(List<RssFeedResult> existingFeedResults, XNamespace tv, XNamespace nyaa)
        => xElement => !existingFeedResults
            .Any(feedResult => (!string.IsNullOrEmpty(feedResult.TvInfoHash) && feedResult.TvInfoHash == xElement.Element(tv + "info_hash")?.Value)
                               || (!string.IsNullOrEmpty(feedResult.NyaaInfoHash) && feedResult.NyaaInfoHash == xElement.Element(nyaa + "infoHash")?.Value));
}