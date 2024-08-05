namespace mlt.common.services;

public interface IRssFeedProcessorService
{
    Task ProcessFeed(string rssFeedId);
}