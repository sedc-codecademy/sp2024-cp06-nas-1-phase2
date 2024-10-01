using AutoMapper;
using DomainModels;
using DTOs.RssSource;

namespace Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //RssSource
            CreateMap<RssSource, RssSourceDto>().ReverseMap();
            CreateMap<RssSource, AddRssSourceDto>().ReverseMap();
            CreateMap<RssSource, UpdateRssSourceDto>().ReverseMap();
        }
    }
}
