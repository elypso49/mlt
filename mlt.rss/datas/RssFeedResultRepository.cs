using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.datas;
using mlt.common.options;
using mlt.rss.datas.models;
using mlt.rss.dtos;
using MongoDB.Driver;

namespace mlt.rss.datas;

internal class RssFeedResultRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    : CrudRepository<RssFeedResult, RssFeedResultModel>(settings, mapper, "RssFeedResults"), IRssFeedResultRepository
{
    public async Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId)
        => Mapper.Map<IEnumerable<RssFeedResult>>((await Collection.FindAsync(result => result.RssFeedId == rssFeedId)).ToList());
}