using mlt.common.domainEntities.RssFeed;

namespace mlt.services.RssFeed;

public interface IRssFeedResultService
{
    Task<IEnumerable<RssFeedResult>> GetAll();

    Task<RssFeedResult> GetById(string id);

    Task Add(RssFeedResult result);

    Task Update(string id, RssFeedResult result);

    Task Delete(string id);
}