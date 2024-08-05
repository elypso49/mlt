using AutoMapper;
using mlt.common.domainEntities;
using mlt.dal.models;

namespace mlt.dal;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
    }
}