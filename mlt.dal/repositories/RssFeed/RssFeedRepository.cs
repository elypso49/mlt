using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.dal.models;
using mlt.dal.Options;

namespace mlt.dal.repositories.RssFeed;

internal class RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    : CrudRepository<common.domainEntities.RssFeed, RssFeedModel>(settings, mapper, "RssFeeds"), IRssFeedRepository;