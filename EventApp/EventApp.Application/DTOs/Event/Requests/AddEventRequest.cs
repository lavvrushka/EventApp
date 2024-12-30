using EventApp.Application.DTOs.Location.Request;
using MediatR;

namespace EventApp.Application.DTOs.Event.Requests
{
    public record AddEventRequest(
        string Title,
        string Description,
        DateTime DateTime,
        LocationRequest Location,
        string Category,
        string ImageData,
        string ImageType,
        int MaxUsers) : IRequest<Unit>;
}

