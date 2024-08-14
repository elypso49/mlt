namespace mlt.rss.repositories;

internal class RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper) : CrudRepository<RssFeed, RssFeedModel>(settings, mapper, "RssFeeds"), IRssFeedRepository;