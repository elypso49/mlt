using mlt.common.domainEntities;
using mlt.dal.repositories.RssFeed;

namespace mlt.services.RssFeed;

public class RssFeedResultService(IRssFeedResultRepository rssFeedResultRepository) : CrudService<RssFeedResult>(rssFeedResultRepository), IRssFeedResultService
{
    public Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId)
        => rssFeedResultRepository.GetByRssFeedId(rssFeedId);

}