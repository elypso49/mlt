using AutoMapper;
using mlt.common.domainEntities;
using mlt.dal.models;
using mlt.dal.resultDtos;

namespace mlt.dal;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
        CreateMap<MongoDB.Driver.DeleteResult, DeleteResult>().ReverseMap();
    }
}