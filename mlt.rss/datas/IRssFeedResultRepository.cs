namespace mlt.rss.datas;

public interface IRssFeedResultRepository : ICrudRepository<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
}