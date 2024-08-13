namespace mlt.rss.services;

public class RssFeedResultService(IRssFeedResultRepository rssFeedResultRepository) : CrudService<RssFeedResult>(rssFeedResultRepository), IRssFeedResultService
{
    public Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId) => rssFeedResultRepository.GetByRssFeedId(rssFeedId);
}