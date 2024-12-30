using AutoMapper;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Application.DTOs.User.Responses;
using EventApp.Domain.Models;

namespace EventApp.Application.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserLoginResponse>();
            CreateMap<User, UserLoginResponse>();
            CreateMap<User, UserLoginResponse>();
            CreateMap<UserRegisterRequest, User>();
            CreateMap<(string AccessToken, string RefreshToken), UserRegisterResponse>()
           .ConstructUsing(tuple => new UserRegisterResponse(tuple.AccessToken, tuple.RefreshToken));
            CreateMap<(string AccessToken, string RefreshToken), UserLoginResponse>()
            .ConstructUsing(tuple => new UserLoginResponse(tuple.AccessToken, tuple.RefreshToken));
            CreateMap<User, UserResponse>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<UserResponse, User>();
            CreateMap<(string AccessToken, string RefreshToken), UserTokenRespones>()
                  .ConstructUsing(tuple => new UserTokenRespones(tuple.AccessToken, tuple.RefreshToken));
        }
    }
}
