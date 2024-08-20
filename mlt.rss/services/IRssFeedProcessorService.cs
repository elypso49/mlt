namespace mlt.rss.services;

public interface IRssFeedProcessorService
{
    Task<RssSyncResult?> ProcessFeed(string rssFeedId);
}