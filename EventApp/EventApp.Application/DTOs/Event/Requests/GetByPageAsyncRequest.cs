using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.DTOs.Event.Requests
{
    public record GetEventsByPageAsyncRequest(
            int PageIndex,
            int PageSize
        ) : IRequest<Pagination<EventResponse>>;

}
