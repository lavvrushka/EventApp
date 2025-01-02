using Microsoft.AspNetCore.Http;
using MediatR;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;

namespace EventApp.Application.UseCases.AuthUsecases
{
    public class LogoutHandler : IRequestHandler<UserLogoutRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Unit> Handle(UserLogoutRequest request, CancellationToken cancellationToken)
        {
            var token = ExtractTokenFromHeader() ?? throw new UnauthorizedAccessException("Token is missing.");
            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(token);

            if (refreshToken != null)
            {
                await _tokenService.RevokeRefreshTokenAsync();
            }

            return Unit.Value;
        }

        private string? ExtractTokenFromHeader()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return null;
            }

            if (authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }

            return null;
        }
    }
}
