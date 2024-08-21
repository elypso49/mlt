using mlt.common.datas;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;

namespace mlt.rss.repositories;

internal interface IRssFeedResultRepository : ICrudRepository<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
    Task<IEnumerable<RssFeedResult>> GetByStatus(StateValue stateValue);
}