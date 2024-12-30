using AutoMapper;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class GetFilterEventHandler : IRequestHandler<FilterEventsRequest, List<EventResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetFilterEventHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

      public async Task<List<EventResponse>> Handle(FilterEventsRequest request, CancellationToken cancellationToken)
      {
           
            var address = request.Address ?? string.Empty;
            var city = request.City ?? string.Empty;
            var state = request.State ?? string.Empty;
            var country = request.Country ?? string.Empty;

            var eventsEntities = await _unitOfWork.Events.GetEventsFilteredAsync(address, city, state, country, request.Category);
            return _mapper.Map<List<EventResponse>>(eventsEntities);
      }

    }
}
