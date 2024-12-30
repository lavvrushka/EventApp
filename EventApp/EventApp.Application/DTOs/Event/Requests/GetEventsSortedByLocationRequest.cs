using EventApp.Application.DTOs.Event.Responses;
using MediatR;

namespace EventApp.Application.DTOs.Event.Requests
{
    public record GetEventsSortedByLocationRequest() : IRequest<List<EventResponse>>;
}