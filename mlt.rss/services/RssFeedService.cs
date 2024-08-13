namespace mlt.rss.services;

public class RssFeedService(IRssFeedRepository rssFeedRepository) : CrudService<RssFeed>(rssFeedRepository), IRssFeedService { }