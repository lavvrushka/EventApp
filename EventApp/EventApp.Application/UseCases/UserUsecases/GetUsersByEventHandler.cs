using AutoMapper;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Application.DTOs.User.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using MediatR;

namespace EventShowcase.Application.UseCases.UserUseCases
{
    public class GetUsersByEventHandler : IRequestHandler<GetUsersByEventRequest, List<UserResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersByEventHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserResponse>> Handle(GetUsersByEventRequest request, CancellationToken cancellationToken)
        {
            var usersEntities = await _unitOfWork.Users.GetByEventAsync(request.IdEvent);
            var userResponse = _mapper.Map<List<UserResponse>>(usersEntities);
            return userResponse;
        }
    }
}
