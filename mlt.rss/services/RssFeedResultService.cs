﻿namespace mlt.rss.services;

internal class RssFeedResultService(IRssFeedResultRepository rssFeedResultRepository) : CrudService<RssFeedResult>(rssFeedResultRepository), IRssFeedResultService
{
    public Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId)
        => rssFeedResultRepository.GetByRssFeedId(rssFeedId);

    public Task<IEnumerable<RssFeedResult>> GetByStatus(StateValue stateValue)
        => rssFeedResultRepository.GetByStatus(stateValue);
}