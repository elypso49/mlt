namespace mlt.rss.services;

public interface IRssFeedResultService : ICrudService<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>?> GetByRssFeedId(string rssFeedId);
    public Task<IEnumerable<RssFeedResult>?> GetByStatus(StateValue stateValue);
}