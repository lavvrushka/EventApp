using AutoMapper;
using EventApp.Application.Common.Interfaces;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class GetEventsSortedByLocationHandler : IRequestHandler<GetEventsSortedByLocationRequest, List<EventResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventsSortedByLocationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventResponse>> Handle(GetEventsSortedByLocationRequest request, CancellationToken cancellationToken)
        {
            var eventsEntities = await _unitOfWork.Events.GetEventsSortedByLocationAsync();
            return _mapper.Map<List<EventResponse>>(eventsEntities);
        }
    }
}
