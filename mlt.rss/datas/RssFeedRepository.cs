namespace mlt.rss.datas;

internal class RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper) : CrudRepository<RssFeed, RssFeedModel>(settings, mapper, "RssFeeds"), IRssFeedRepository;