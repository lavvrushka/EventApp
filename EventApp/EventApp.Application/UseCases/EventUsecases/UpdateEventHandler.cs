using AutoMapper;
using EventApp.Application.Common.Exeptions;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Interfaces.IServices;
using EventApp.Domain.ValueObjects;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{

    public class UpdateEventHandler : IRequestHandler<UpdateEventRequest, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailNotificationService _emailNotificationService;

        public UpdateEventHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailNotificationService emailNotificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        
            _emailNotificationService = emailNotificationService;
        }

        public async Task<Unit> Handle(UpdateEventRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = await _unitOfWork.Events.GetByIdAsync(request.Id);
            if (eventEntity == null)
            {
                throw new NotFoundException("Event", request.Id);
            }

            var image = await _unitOfWork.Images.GetByIdAsync(request.ImageId);
            if (image == null)
            {
                throw new NotFoundException("Image", request.ImageId);
            }

            _mapper.Map(request, eventEntity);
            eventEntity.Location = new Location
            {
                Address = request.Location.Address,
                City = request.Location.City,
                State = request.Location.State,
                Country = request.Location.Country
            };

            _mapper.Map(request, image);
            eventEntity.ImageId = image.Id;

            await _unitOfWork.SaveChangesAsync();
            await _emailNotificationService.SendEventUpdateNotificationAsync(eventEntity);

            return Unit.Value;
        }

    }

}
