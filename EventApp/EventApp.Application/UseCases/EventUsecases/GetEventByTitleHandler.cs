using AutoMapper;
using EventApp.Application.Common.Exeptions;
using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Intarfaces.IRepositories;
using EventApp.Domain.Models;
using MediatR;

namespace EventApp.Application.UseCases.EventUsecases
{
    public class GetEventByTitleHandler : IRequestHandler<GetEventByTitleRequest, EventResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEventByTitleHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EventResponse> Handle(GetEventByTitleRequest request, CancellationToken cancellationToken)
        {
            var eventEntity = await _unitOfWork.Events.GetEventByTitleAsync(request.Title)
                ?? throw new NotFoundException(nameof(Event), request.Title);

            return _mapper.Map<EventResponse>(eventEntity);
        }
    }
}
