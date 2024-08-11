using mlt.common.domainEntities;

namespace mlt.dal.repositories.RssFeed;

public interface IRssFeedResultRepository : ICrudRepository<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
}