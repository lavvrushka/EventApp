using EventApp.Application.DTOs.Event.Responses;
using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record GetUserEventsRequest() : IRequest<List<EventResponse>>;
}
