namespace mlt.rss.services;

internal class RssFeedService(IRssFeedRepository rssFeedRepository) : CrudService<RssFeed>(rssFeedRepository), IRssFeedService { }