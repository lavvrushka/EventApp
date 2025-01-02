using EventApp.Application.DTOs.User.Requests;
using EventApp.Domain.Models;
using MediatR;
using EventApp.Application.Common.Exeptions;
using EventApp.Domain.Interfaces.IServices;
using EventApp.Domain.Intarfaces.IRepositories;

namespace EventApp.Application.UseCases.UserUsecases
{
    public class RegisterUserToEventHandler : IRequestHandler<RegisterUserToEventRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public RegisterUserToEventHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(RegisterUserToEventRequest request, CancellationToken cancellationToken)
        {

            var token = _tokenService.ExtractTokenFromHeader();

            User userEntity;
            userEntity = await AuthenticateUserAsync(token);
            
            var eventItem = await _unitOfWork.Events.GetByIdAsync(request.IdEvent)
                ?? throw new NotFoundException("Event", request.IdEvent);

            if (eventItem.Users.Any(u => u.Id == userEntity.Id))
            {
                throw new InvalidOperationException("User is already registered for this event.");
            }

            await _unitOfWork.Users.RegisterUserToEventAsync(request.IdEvent, userEntity.Id);
            await _unitOfWork.Events.UpdateAsync(eventItem);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
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
