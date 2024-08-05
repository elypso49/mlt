using mlt.common.domainEntities;

namespace mlt.dal.repositories;

public interface IRssFeedResultRepository
{
    Task<IEnumerable<RssFeedResult>> GetAll();
    Task<RssFeedResult> GetById(string id);
    Task Add(RssFeedResult result);
    Task Update(string id, RssFeedResult result);
    Task Delete(string id);
}