using mlt.common.services;
using mlt.rss.datas;
using mlt.rss.dtos;

namespace mlt.rss.services;

public class RssFeedService(IRssFeedRepository rssFeedRepository) : CrudService<RssFeed>(rssFeedRepository), IRssFeedService { }