namespace mlt.rss.datas;

public interface IRssFeedProcessorRepository
{
    Task ProcessFeed(string rssFeedId);
}