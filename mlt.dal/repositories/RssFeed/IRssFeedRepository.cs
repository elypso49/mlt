using mlt.dal.resultDtos;

namespace mlt.dal.repositories.RssFeed;

public interface IRssFeedRepository
{
    Task<IEnumerable<common.domainEntities.RssFeed.RssFeed>> GetAll();

    Task<common.domainEntities.RssFeed.RssFeed> GetById(string id);

    Task<common.domainEntities.RssFeed.RssFeed> Add(common.domainEntities.RssFeed.RssFeed feed);

    Task<UpdateResult> Update(string id, common.domainEntities.RssFeed.RssFeed feed);

    Task<DeleteResult> Delete(string id);
}