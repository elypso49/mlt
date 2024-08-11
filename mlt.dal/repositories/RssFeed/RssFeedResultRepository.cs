using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.domainEntities;
using mlt.dal.models;
using mlt.dal.Options;
using MongoDB.Driver;

namespace mlt.dal.repositories.RssFeed;

internal class RssFeedResultRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    : CrudRepository<RssFeedResult, RssFeedResultModel>(settings, mapper, "RssFeedResults"), IRssFeedResultRepository
{
    public async Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId)
        => Mapper.Map<IEnumerable<RssFeedResult>>((await Collection.FindAsync(result => result.RssFeedId == rssFeedId)).ToList());
}