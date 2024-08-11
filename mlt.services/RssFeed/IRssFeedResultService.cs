using mlt.common.domainEntities;

namespace mlt.services.RssFeed;

public interface IRssFeedResultService : ICrudService<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
}