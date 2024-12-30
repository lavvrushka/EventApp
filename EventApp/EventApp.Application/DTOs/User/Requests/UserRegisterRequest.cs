using EventApp.Application.DTOs.User.Responses;
using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record UserRegisterRequest
    (
         string Firstname,
         string Lastname,
         string Password,  
         string Email,
         DateTime BirthDate 

    ) : IRequest<UserRegisterResponse>;
}
