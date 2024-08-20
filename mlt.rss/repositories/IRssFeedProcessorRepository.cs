namespace mlt.rss.repositories;

internal interface IRssFeedProcessorRepository
{
    Task<RssSyncResult?> ProcessFeed(string rssFeedId);
}