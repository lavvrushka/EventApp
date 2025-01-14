using EventApp.Application.DTOs.User.Responses;
using EventApp.Application.DTOs.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;

namespace EventApp.Application.UseCases.UserUsecases
{
    public class CurrentUserHandler : IRequestHandler<CurrentUserRequest, UserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CurrentUserHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<UserResponse> Handle(CurrentUserRequest request, CancellationToken cancellationToken)
        {
            
            var token = _tokenService.ExtractTokenFromHeader();
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException("Token is missing.");
            }

           
            var userId = _tokenService.ExtractUserIdFromToken(token);
            if (!userId.HasValue)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            
            var user = await _unitOfWork.Users.GetByIdAsync(userId.Value);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found.");
            }
            
            var userResponse = _mapper.Map<UserResponse>(user);
            return userResponse;
        }
    }
}
