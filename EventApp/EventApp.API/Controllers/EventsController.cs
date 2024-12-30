using EventApp.Application.DTOs.Event.Requests;
using EventApp.Application.DTOs.Event.Responses;
using EventApp.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<EventResponse>>> GetAllEvents()
        {
            var events = await _mediator.Send(new GetAllEventsRequest());
            return Ok(events);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetEventById(Guid id)
        {
            var request = new GetEventByIdRequest(id);  
            var eventResponse = await _mediator.Send(request);
            return Ok(eventResponse);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateEvent([FromBody] AddEventRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventRequest request)
        {
            await _mediator.Send(request);
            return Ok(new { Message = "Event updated successfully" });
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var request = new DeleteEventRequest(id);
            await _mediator.Send(request);
            return NoContent();
        }


        [HttpGet("filter")]
        public async Task<ActionResult<List<EventResponse>>> GetFilteredEvents([FromQuery] FilterEventsRequest request)
        {
            var events = await _mediator.Send(request);
            return Ok(events);
        }

        [HttpGet("by-date")]
        public async Task<ActionResult<EventResponse>> GetEventByDate([FromQuery] DateTime date)
        {
            var request = new GetEventByDateRequest(date);
            var eventResponse = await _mediator.Send(request);
            return Ok(eventResponse);
        }

        [HttpGet("by-title")]
        public async Task<ActionResult<EventResponse>> GetEventByTitle([FromQuery] string title)
        {
            var request = new GetEventByTitleRequest(title);
            var eventResponse = await _mediator.Send(request);
            return Ok(eventResponse);
        }

        [HttpGet("page")]
        public async Task<ActionResult<Pagination<EventResponse>>> GetEventsByPage([FromQuery] GetEventsByPageAsyncRequest request)
        {
            var pagedEvents = await _mediator.Send(request);
            return Ok(pagedEvents);
        }

        [HttpGet("sorted-by-category")]
        public async Task<ActionResult<List<EventResponse>>> GetEventsSortedByCategory()
        {
            var request = new GetEventsSortedByCategoryRequest();
            var sortedEvents = await _mediator.Send(request);
            return Ok(sortedEvents);
        }

        [HttpGet("sorted-by-location")]
        public async Task<ActionResult<List<EventResponse>>> GetEventsSortedByLocation()
        {
            var request = new GetEventsSortedByLocationRequest();
            var sortedEvents = await _mediator.Send(request);
            return Ok(sortedEvents);
        }
    }
}
