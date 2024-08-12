namespace mlt.rss.services;

public interface IRssFeedProcessorService
{
    Task ProcessFeed(string rssFeedId);
}