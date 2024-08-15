namespace mlt.rss.repositories;

internal interface IRssFeedResultRepository : ICrudRepository<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
}