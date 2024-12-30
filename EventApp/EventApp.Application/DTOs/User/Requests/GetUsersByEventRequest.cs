using EventApp.Application.DTOs.User.Responses;
using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record GetUsersByEventRequest(Guid IdEvent) : IRequest<List<UserResponse>>;
}
