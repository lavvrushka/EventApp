using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Domain.Models;

namespace EventApp.Application.Common.Mappings
{
    public class PageSettingsProfile : Profile
    {
        public PageSettingsProfile()
        {
            CreateMap<GetEventsByPageAsyncRequest, PageSettings>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
            CreateMap<GetUsersByPageAsyncRequest, PageSettings>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(src => src.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize));
        }
    }
}
