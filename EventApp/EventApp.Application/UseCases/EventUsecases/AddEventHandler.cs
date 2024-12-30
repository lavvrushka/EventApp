using AutoMapper;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Domain.Models;
using EventApp.Domain.ValueObjects;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class AddEventHandler : IRequestHandler<AddEventRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddEventHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddEventRequest request, CancellationToken cancellationToken)
        {
            
            var location = _mapper.Map<Location>(request.Location);
            var eventEntity = _mapper.Map<Event>(request);
            eventEntity.Location = location;
            var image = _mapper.Map<Image>(request);
            var imageId = await _unitOfWork.Images.AddImageToEventAsync(image);
            eventEntity.ImageId = imageId;
            await _unitOfWork.Events.AddAsync(eventEntity);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
