using EventApp.Application.DTOs.User.Responses;
using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record UserLoginRequest(
         string Email,
         string Password
     ) : IRequest<UserLoginResponse>;

}
