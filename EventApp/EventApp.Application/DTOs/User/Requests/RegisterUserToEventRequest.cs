using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record RegisterUserToEventRequest(Guid IdEvent) : IRequest<Unit>;
}
