namespace mlt.rss.services;

public interface IRssFeedResultService : ICrudService<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
}