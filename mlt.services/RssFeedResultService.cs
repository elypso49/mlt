using mlt.common.domainEntities;
using mlt.common.services;
using mlt.dal.repositories;

namespace mlt.services;

public class RssFeedResultService(IRssFeedResultRepository rssFeedResultRepository) : IRssFeedResultService
{
    public Task<IEnumerable<RssFeedResult>> GetAll()
        => rssFeedResultRepository.GetAll();

    public Task<RssFeedResult> GetById(string id)
        => rssFeedResultRepository.GetById(id);

    public Task Add(RssFeedResult result)
        => rssFeedResultRepository.Add(result);

    public Task Update(string id, RssFeedResult result)
        => rssFeedResultRepository.Update(id, result);

    public Task Delete(string id)
        => rssFeedResultRepository.Delete(id);
}