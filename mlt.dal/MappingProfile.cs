using AutoMapper;
using mlt.common.domainEntities.RssFeed;
using mlt.dal.models.RssFeed;

namespace mlt.dal;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
    }
}