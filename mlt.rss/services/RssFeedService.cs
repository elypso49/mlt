using mlt.common.dtos.rss;
using mlt.common.services;
using mlt.rss.repositories;

namespace mlt.rss.services;

internal class RssFeedService(IRssFeedRepository rssFeedRepository) : CrudService<RssFeed>(rssFeedRepository), IRssFeedService { }