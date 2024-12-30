using AutoMapper;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Application.DTOs.User.Responses;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.UserUsecases
{
    public class GetUsersByPageHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetUsersByPageAsyncRequest, Pagination<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Pagination<UserResponse>> Handle(GetUsersByPageAsyncRequest request, CancellationToken cancellationToken)
        {
            var pageSettings = _mapper.Map<PageSettings>(request);
            var users = await _unitOfWork.Users.GetByPageAsync(pageSettings);
            var totalCount = await _unitOfWork.Users.GetUserCountAsync();
            var userResponse = _mapper.Map<List<UserResponse>>(users);
            return new Pagination<UserResponse>(userResponse, totalCount, pageSettings);
        }
    }
}
