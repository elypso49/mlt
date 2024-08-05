using mlt.common.services;
using mlt.dal.repositories;

namespace mlt.services;

public class RssFeedProcessorService(IRssFeedProcessorRepository rssFeedProcessorRepository) : IRssFeedProcessorService
{
    public Task ProcessFeed(string rssFeedId)
        => rssFeedProcessorRepository.ProcessFeed(rssFeedId);
}