using mlt.common.domainEntities;

namespace mlt.common.services;

public interface IRssFeedService
{
    public Task<IEnumerable<RssFeed>> GetAll();
    public Task<RssFeed> GetById(string id);
    public Task Add(RssFeed feed);
    public Task Update(string id, RssFeed feed);
    public Task Delete(string id);
}