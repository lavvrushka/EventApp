using AutoMapper;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class GetEventByPageHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetEventsByPageAsyncRequest, Pagination<EventResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        public async Task<Pagination<EventResponse>> Handle(GetEventsByPageAsyncRequest request, CancellationToken cancellationToken)
        {
            var pageSettings = _mapper.Map<PageSettings>(request);
            var events = await _unitOfWork.Events.GetByPageAsync(pageSettings);
            var totalCount = await _unitOfWork.Events.GetEventCountAsync();
            var eventResponse = _mapper.Map<List<EventResponse>>(events);
            return new Pagination<EventResponse>(eventResponse, totalCount, pageSettings);
        }
    }
}
