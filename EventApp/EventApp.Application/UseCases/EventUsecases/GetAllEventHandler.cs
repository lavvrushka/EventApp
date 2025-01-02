using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class GetAllEventsHandler : IRequestHandler<GetAllEventsRequest, List<EventResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllEventsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventResponse>> Handle(GetAllEventsRequest request, CancellationToken cancellationToken)
        {
            var eventsEntities = await _unitOfWork.Events.GetAllAsync();

            if (eventsEntities == null || !eventsEntities.Any())
            {
                throw new Exception("No events found.");
            }

            var eventResponse = _mapper.Map<List<EventResponse>>(eventsEntities);
            return eventResponse;
        }

    }
}
