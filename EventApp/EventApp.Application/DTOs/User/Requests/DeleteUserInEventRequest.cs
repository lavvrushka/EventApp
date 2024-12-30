using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record DeleteUserInEventRequest(
          Guid IdEvent) : IRequest<Unit>;
}
