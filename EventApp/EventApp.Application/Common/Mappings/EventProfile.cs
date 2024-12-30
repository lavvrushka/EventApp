using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Domain.Models;
using EventApp.Application.DTOs.Event.Responses;

namespace EventApp.Application.Common.Mappings
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<AddEventRequest, Event>();
            CreateMap<UpdateEventRequest, Event>();

            CreateMap<Event, EventResponse>()
            .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.Image != null ? src.Image.ImageData : null))
            .ForMember(dest => dest.ImageType, opt => opt.MapFrom(src => src.Image != null ? src.Image.ImageType : null))
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users));

            CreateMap<Image, AddEventRequest>().ReverseMap();
            CreateMap<Image, UpdateEventRequest>().ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ImageData, opt => opt.Condition(src => src.ImageData != null))
            .ForMember(dest => dest.ImageType, opt => opt.Condition(src => src.ImageType != null));

        }

    }
}
