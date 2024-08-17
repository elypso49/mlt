namespace mlt.rss.repositories;

internal interface IRssFeedProcessorRepository
{
    Task ProcessFeed(string rssFeedId);
}