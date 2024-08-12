using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.datas;
using mlt.common.options;
using mlt.rss.datas.models;
using mlt.rss.dtos;

namespace mlt.rss.datas;

internal class RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    : CrudRepository<RssFeed, RssFeedModel>(settings, mapper, "RssFeeds"), IRssFeedRepository;