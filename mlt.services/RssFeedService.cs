using mlt.common.domainEntities;
using mlt.common.services;
using mlt.dal.repositories;

namespace mlt.services;

public class RssFeedService(IRssFeedRepository rssFeedRepository) : IRssFeedService
{
    public Task<IEnumerable<RssFeed>> GetAll()
        => rssFeedRepository.GetAll();

    public Task<RssFeed> GetById(string id)
        => rssFeedRepository.GetById(id);

    public Task Add(RssFeed feed)
        => rssFeedRepository.Add(feed);

    public async Task Update(string id, RssFeed feed)
    {
        // Makes sure the feed has the id
        feed.Id = id;
        await rssFeedRepository.Update(id, feed);
    }

    public Task Delete(string id)
        => rssFeedRepository.Delete(id);
}