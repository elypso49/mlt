using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.datas;
using mlt.common.dtos.rss;
using mlt.common.options;
using mlt.rss.repositories.models;

namespace mlt.rss.repositories;

internal class RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper) : CrudRepository<RssFeed, RssFeedModel>(settings, mapper, "RssFeeds"), IRssFeedRepository;