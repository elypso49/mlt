using mlt.common.services;
using mlt.rss.dtos;

namespace mlt.rss.services;

public interface IRssFeedResultService : ICrudService<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
}