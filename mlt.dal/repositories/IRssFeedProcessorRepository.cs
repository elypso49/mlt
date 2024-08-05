namespace mlt.dal.repositories;

public interface IRssFeedProcessorRepository
{
    Task ProcessFeed(string rssFeedId);
}