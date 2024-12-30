using Microsoft.AspNetCore.Mvc;
using EventApp.Application.DTOs.User.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace EventApp.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
            => Ok(await _mediator.Send(request));

        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
            => Ok(await _mediator.Send(request));

       
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _mediator.Send(new UserLogoutRequest());
            return NoContent();
        }

        
        [HttpDelete("remove-from-event")]
        public async Task<IActionResult> DeleteUserFromEvent([FromBody] DeleteUserInEventRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        
        [HttpGet("events")]
        public async Task<IActionResult> GetUserEvents([FromQuery] GetUserEventsRequest request)
        {
            var events = await _mediator.Send(request);
            return Ok(events);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("event-users/{eventId}")]
        public async Task<IActionResult> GetUsersByEvent([FromRoute] Guid eventId)
        {
            var request = new GetUsersByEventRequest(eventId);
            var users = await _mediator.Send(request);
            return Ok(users);
        }

        
        [HttpPost("register-to-event")]
        public async Task<IActionResult> RegisterUserToEvent([FromBody] RegisterUserToEventRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        
        [HttpGet("paginate-users")]
        public async Task<IActionResult> GetUsersByPage([FromQuery] GetUsersByPageAsyncRequest request)
        {
            var users = await _mediator.Send(request);
            return Ok(users);
        }

        
        [HttpGet("current")]
        [Authorize]  
        public async Task<IActionResult> GetCurrentUser()
        {
            var request = new CurrentUserRequest(); 
            var userResponse = await _mediator.Send(request);
            return Ok(userResponse);
        }

        [HttpPost("refresh-token")]
   
        public async Task<IActionResult> RefreshToken([FromBody] UserRefreshTokenRequest request)
        {
            var userTokenResponse = await _mediator.Send(request);
            return Ok(userTokenResponse);
        }

    }
}
