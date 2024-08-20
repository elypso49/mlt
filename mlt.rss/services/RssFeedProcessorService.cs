using mlt.common.dtos.responses;
using mlt.common.dtos.rss;
using mlt.common.services;
using mlt.rss.repositories;

namespace mlt.rss.services;

internal class RssFeedProcessorService(IRssFeedProcessorRepository rssFeedProcessorRepository) : BaseService, IRssFeedProcessorService
{
    public Task<ResponseDto<RssSyncResult>> ProcessFeed(string rssFeedId)
        => HandleDataRetrievement(async () => await rssFeedProcessorRepository.ProcessFeed(rssFeedId));
}