using mlt.common.dtos.responses;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;
using mlt.common.services;
using mlt.rss.repositories;

namespace mlt.rss.services;

internal class RssFeedResultService(IRssFeedResultRepository rssFeedResultRepository) : CrudService<RssFeedResult>(rssFeedResultRepository), IRssFeedResultService
{
    public Task<ResponseDto<IEnumerable<RssFeedResult>>> GetByRssFeedId(string rssFeedId)
        => HandleDataRetrievement(async () => await rssFeedResultRepository.GetByRssFeedId(rssFeedId));

    public Task<ResponseDto<IEnumerable<RssFeedResult>>> GetByStatus(StateValue stateValue)
        => HandleDataRetrievement(async () => await rssFeedResultRepository.GetByStatus(stateValue));
}