namespace mlt.rss.repositories;

internal class RssFeedResultRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    : CrudRepository<RssFeedResult, RssFeedResultModel>(settings, mapper, "RssFeedResults"), IRssFeedResultRepository
{
    public async Task<IEnumerable<RssFeedResult>?> GetByRssFeedId(string rssFeedId)
        => Mapper.Map<IEnumerable<RssFeedResult>>(await Collection.FindAsync(result => result.RssFeedId == rssFeedId));

    public async Task<IEnumerable<RssFeedResult>?> GetByStatus(StateValue stateValue)
        => Mapper.Map<IEnumerable<RssFeedResult>>(await Collection.FindAsync(result => result.State == stateValue));
}