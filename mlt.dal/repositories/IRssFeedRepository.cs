using mlt.common.domainEntities;

namespace mlt.dal.repositories;

public interface IRssFeedRepository
{
    Task<IEnumerable<RssFeed>> GetAll();

    Task<RssFeed> GetById(string id);

    Task<RssFeed> Add(RssFeed feed);

    Task<UpdateResult> Update(string id, RssFeed feed);

    Task<DeleteResult> Delete(string id);
}