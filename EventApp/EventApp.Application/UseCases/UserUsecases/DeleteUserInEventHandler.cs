using EventApp.Application.Common.Exeptions;
using EventApp.Application.DTOs.User.Requests;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.UserUsecases
{
    public class DeleteUserInEventHandler : IRequestHandler<DeleteUserInEventRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public DeleteUserInEventHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(DeleteUserInEventRequest request, CancellationToken cancellationToken)
        {
            var token = _tokenService.ExtractTokenFromHeader();

            User userEntity = await _tokenService.AuthenticateUserAsync(token);

            if (userEntity == null)
            {
                throw new KeyNotFoundException($"User with ID {userEntity.Id} not found.");
            }

            var eventUsers = await _unitOfWork.Users.GetByEventAsync(request.IdEvent);

            if (!eventUsers.Any(u => u.Id == userEntity.Id))
            {
                throw new UnauthorizedAccessException($"User with ID {userEntity.Id} is not a participant of the event.");
            }

            await _unitOfWork.Users.DeleteInEventAsync(userEntity.Id, request.IdEvent);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
