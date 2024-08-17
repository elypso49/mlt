namespace mlt.rss.services;

internal class RssFeedProcessorService(IRssFeedProcessorRepository rssFeedProcessorRepository) : IRssFeedProcessorService
{
    public Task ProcessFeed(string rssFeedId)
        => rssFeedProcessorRepository.ProcessFeed(rssFeedId);
}