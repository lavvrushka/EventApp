using EventApp.Application.DTOs.User.Requests;
using EventApp.Application.DTOs.User.Responses;
using MediatR;
using AutoMapper;
using EventApp.Domain.Interfaces.IServices;

namespace EventApp.Application.UseCases.AuthUsecases
{
    public class RefreshTokenHandler : IRequestHandler<UserRefreshTokenRequest, UserTokenRespones>
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public RefreshTokenHandler(ITokenService tokenService, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<UserTokenRespones> Handle(UserRefreshTokenRequest request, CancellationToken cancellationToken)
        {
   
            var (newAccessToken, newRefreshToken) = await _tokenService.RefreshTokensAsync(request.RefreshToken);

            return _mapper.Map<UserTokenRespones>((newAccessToken, newRefreshToken));
        }
    }
}
