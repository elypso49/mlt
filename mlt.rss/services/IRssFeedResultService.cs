using mlt.common.dtos.responses;
using mlt.common.dtos.rss;
using mlt.common.dtos.rss.enums;
using mlt.common.services;

namespace mlt.rss.services;

public interface IRssFeedResultService : ICrudService<RssFeedResult>
{
    Task<ResponseDto<IEnumerable<RssFeedResult>>> GetByRssFeedId(string rssFeedId);
    public Task<ResponseDto<IEnumerable<RssFeedResult>>> GetByStatus(StateValue stateValue);
}