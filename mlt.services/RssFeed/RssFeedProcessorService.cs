using mlt.dal.repositories.RssFeed;

namespace mlt.services.RssFeed;

public class RssFeedProcessorService(IRssFeedProcessorRepository rssFeedProcessorRepository) : IRssFeedProcessorService
{
    public Task ProcessFeed(string rssFeedId)
        => rssFeedProcessorRepository.ProcessFeed(rssFeedId);
}