using MediatR;


namespace EventApp.Application.DTOs.User.Requests
{

    public record UserLogoutRequest : IRequest<Unit>;
}
