using AutoMapper;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Application.DTOs.User.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.AuthUsecases
{
    public class RegisterUserHandler : IRequestHandler<UserRegisterRequest, UserRegisterResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IHashPassword _hashPassword;
        private readonly IMapper _mapper;

        public RegisterUserHandler(IUnitOfWork unitOfWork, ITokenService tokenService, IHashPassword hashPassword, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _hashPassword = hashPassword;
            _mapper = mapper;
        }

        public async Task<UserRegisterResponse> Handle(UserRegisterRequest request, CancellationToken cancellationToken)
        {
            var existingProfile = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingProfile != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            var role = await _unitOfWork.Roles.GetByNameAsync("User");
            var hashedPassword = _hashPassword.Hash(request.Password);

            var newUser = new User
            {
                FirstName = request.Firstname,
                LastName = request.Lastname,
                Password = hashedPassword,
                BirthDate = request.BirthDate,
                Email = request.Email,
                RoleId = role.Id
            };

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();

            var accessToken = await _tokenService.GenerateAccessToken(newUser);
            var refreshToken = await _tokenService.GenerateRefreshTokenAsync(newUser.Id);


            return _mapper.Map<UserRegisterResponse>((accessToken, refreshToken));
        }
    }
}
