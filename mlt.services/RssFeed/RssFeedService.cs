using mlt.dal.repositories.RssFeed;

namespace mlt.services.RssFeed;

public class RssFeedService(IRssFeedRepository rssFeedRepository) : IRssFeedService
{
    public Task<IEnumerable<common.domainEntities.RssFeed.RssFeed>> GetAll()
        => rssFeedRepository.GetAll();

    public Task<common.domainEntities.RssFeed.RssFeed> GetById(string id)
        => rssFeedRepository.GetById(id);

    public Task Add(common.domainEntities.RssFeed.RssFeed feed)
        => rssFeedRepository.Add(feed);

    public async Task Update(string id, common.domainEntities.RssFeed.RssFeed feed)
    {
        feed.Id = id;
        await rssFeedRepository.Update(id, feed);
    }

    public Task Delete(string id)
        => rssFeedRepository.Delete(id);
}