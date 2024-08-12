using mlt.common.services;
using mlt.rss.datas;
using mlt.rss.dtos;

namespace mlt.rss.services;

public class RssFeedResultService(IRssFeedResultRepository rssFeedResultRepository) : CrudService<RssFeedResult>(rssFeedResultRepository), IRssFeedResultService
{
    public Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId)
        => rssFeedResultRepository.GetByRssFeedId(rssFeedId);

}