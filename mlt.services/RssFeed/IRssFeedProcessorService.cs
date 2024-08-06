namespace mlt.services.RssFeed;

public interface IRssFeedProcessorService
{
    Task ProcessFeed(string rssFeedId);
}