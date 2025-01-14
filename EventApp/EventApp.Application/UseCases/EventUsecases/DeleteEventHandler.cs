using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class DeleteEventHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<DeleteEventRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Unit> Handle(DeleteEventRequest deleteEventRequest, CancellationToken cancellationToken)
        {
       
            var entity = _mapper.Map<Event>(deleteEventRequest);
            _unitOfWork.Events.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
