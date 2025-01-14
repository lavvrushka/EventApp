using MediatR;

namespace EventApp.Application.DTOs.Event.Requests
{
    public record DeleteEventRequest(Guid Id) : IRequest<Unit>;

}
