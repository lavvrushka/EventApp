using AutoMapper;
using EventApp.Application.Common.Exeptions;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.UserUsecases
{
    public class GetUserEventHandler : IRequestHandler<GetUserEventsRequest, List<EventResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
       
        public GetUserEventHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITokenService tokenService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenService = tokenService;
            
        }

        public async Task<List<EventResponse>> Handle(GetUserEventsRequest request, CancellationToken cancellationToken)
        {
            var token = _tokenService.ExtractTokenFromHeader();

            User userEntity;
            userEntity = await AuthenticateUserAsync(token);

            var user = await _unitOfWork.Users.GetUserEventsAsync(userEntity.Id);

            if (user == null || user.Events == null || !user.Events.Any())
            {
                return new List<EventResponse>();
            }

            return _mapper.Map<List<EventResponse>>(user.Events);
        }
        private async Task<User> AuthenticateUserAsync(string token)
        {
            var userId = _tokenService.ExtractUserIdFromToken(token)
                ?? throw new UnauthorizedAccessException("Invalid token.");

            var user = await _unitOfWork.Users.GetByIdAsync(userId)
                ?? throw new NotFoundException("User", userId);

            return user;
        }
    }
}

