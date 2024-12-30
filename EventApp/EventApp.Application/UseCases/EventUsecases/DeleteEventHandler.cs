using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class DeleteEventHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteEventRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
       

        public async Task<Unit> Handle(DeleteEventRequest deleteEventRequest, CancellationToken cancellationToken)
        {
            var _event = await _unitOfWork.Events.GetByIdAsync(deleteEventRequest.idEvent);
            await _unitOfWork.Events.DeleteAsync(_event);
            await _unitOfWork.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
