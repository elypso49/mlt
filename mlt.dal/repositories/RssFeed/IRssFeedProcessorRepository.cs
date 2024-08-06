namespace mlt.dal.repositories.RssFeed;

public interface IRssFeedProcessorRepository
{
    Task ProcessFeed(string rssFeedId);
}