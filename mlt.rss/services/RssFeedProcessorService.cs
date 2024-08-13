namespace mlt.rss.services;

public class RssFeedProcessorService(IRssFeedProcessorRepository rssFeedProcessorRepository) : IRssFeedProcessorService
{
    public Task ProcessFeed(string rssFeedId) => rssFeedProcessorRepository.ProcessFeed(rssFeedId);
}