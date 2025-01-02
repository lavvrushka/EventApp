using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using MediatR;
using System.Linq.Expressions;

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
            Expression<Func<Event, bool>> filter = e =>
                (string.IsNullOrEmpty(request.City) || e.Location.City == request.City) &&
                (string.IsNullOrEmpty(request.Country) || e.Location.Country == request.Country) &&
                (string.IsNullOrEmpty(request.Address) || e.Location.Address == request.Address) &&
                (string.IsNullOrEmpty(request.State) || e.Location.State == request.State) &&
                (string.IsNullOrEmpty(request.Category) || e.Category.ToLower().Contains(request.Category.ToLower()));

            var eventsEntities = await _unitOfWork.Events.GetFilteredAsync(filter);
            return _mapper.Map<List<EventResponse>>(eventsEntities);
        }
    }
}
