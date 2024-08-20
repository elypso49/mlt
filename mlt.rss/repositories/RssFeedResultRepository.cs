using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.datas;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;
using mlt.common.options;
using mlt.rss.repositories.models;
using MongoDB.Driver;

namespace mlt.rss.repositories;

internal class RssFeedResultRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    : CrudRepository<RssFeedResult, RssFeedResultModel>(settings, mapper, "RssFeedResults"), IRssFeedResultRepository
{
    public async Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId)
        => Mapper.Map<IEnumerable<RssFeedResult>>(await Collection.FindAsync(result => result.RssFeedId == rssFeedId));

    public async Task<IEnumerable<RssFeedResult>> GetByStatus(StateValue stateValue)
        => Mapper.Map<IEnumerable<RssFeedResult>>(await Collection.FindAsync(result => result.State == stateValue));
}