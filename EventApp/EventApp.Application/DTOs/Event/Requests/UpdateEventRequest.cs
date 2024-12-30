using EventApp.Application.DTOs.Location.Request;
using MediatR;


namespace EventApp.Application.DTOs.Event.Requests
{
    public record UpdateEventRequest(
        Guid Id,
        string Title,
        string Description,
        DateTime DateTime,
        LocationRequest Location,
        string Category,
        Guid ImageId,
        string ImageData,
        string ImageType,
        
        int MaxUsers) : IRequest<Unit>;

}
