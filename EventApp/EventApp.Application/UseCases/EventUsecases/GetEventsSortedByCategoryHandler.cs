using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class GetEventsSortedByCategoryHandler : IRequestHandler<GetEventsSortedByCategoryRequest, List<EventResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventsSortedByCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EventResponse>> Handle(GetEventsSortedByCategoryRequest request, CancellationToken cancellationToken)
        {
            var eventsEntities = await _unitOfWork.Events.GetEventsSortedByCategoryAsync();
            return _mapper.Map<List<EventResponse>>(eventsEntities);
        }
    }
}
