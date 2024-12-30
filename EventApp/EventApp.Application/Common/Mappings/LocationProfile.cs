using AutoMapper;
using EventApp.Application.DTOs.Location.Request;
using EventApp.Application.DTOs.Location.Response;
using EventApp.Domain.ValueObjects;

namespace EventApp.Application.Common.Mappings
{
    public class LocationProfile : Profile
    {
        public LocationProfile()
        {
            
            CreateMap<Location, LocationResponse>();
            CreateMap<Location, Location>();
            CreateMap<LocationRequest, Location>();
        }
    }
}
