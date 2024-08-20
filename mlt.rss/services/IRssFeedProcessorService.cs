using mlt.common.dtos.responses;
using mlt.common.dtos.rss;

namespace mlt.rss.services;

public interface IRssFeedProcessorService
{
    Task<ResponseDto<RssSyncResult>> ProcessFeed(string rssFeedId);
}