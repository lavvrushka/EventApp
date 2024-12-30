using EventApp.Application.DTOs.User.Responses;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.DTOs.User.Requests
{
    public record GetUsersByPageAsyncRequest(
          int PageIndex,
          int PageSize
      ) : IRequest<Pagination<UserResponse>>;
}
