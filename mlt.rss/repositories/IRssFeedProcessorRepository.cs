namespace mlt.rss.repositories;

public interface IRssFeedProcessorRepository
{
    Task ProcessFeed(string rssFeedId);
}