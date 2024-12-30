using EventApp.Application.DTOs.Event.Responses;
using MediatR;

namespace EventApp.Application.DTOs.Event.Requests
{
    public record FilterEventsRequest(

       string? Address,
        string? City,
        string? State,
        string? Country, 
        string? Category) : IRequest<List<EventResponse>>;  
}
