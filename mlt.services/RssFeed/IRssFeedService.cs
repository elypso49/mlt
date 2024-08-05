namespace mlt.services.RssFeed;

public interface IRssFeedService
{
    public Task<IEnumerable<common.domainEntities.RssFeed.RssFeed>> GetAll();
    public Task<common.domainEntities.RssFeed.RssFeed> GetById(string id);
    public Task Add(common.domainEntities.RssFeed.RssFeed feed);
    public Task Update(string id, common.domainEntities.RssFeed.RssFeed feed);
    public Task Delete(string id);
}