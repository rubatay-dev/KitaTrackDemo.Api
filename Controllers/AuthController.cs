using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;

namespace KitaTrackDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            var result = await _service.Register(request);
            if (!result.IsSuccess)
                return HandleError(StatusCodes.Status400BadRequest, result.Error);

            return Ok(result.Value);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            var result = await _service.Login(request);
            if (!result.IsSuccess)
                return HandleError(StatusCodes.Status401Unauthorized, result.Error);

            return Ok(result.Value);
        }

        private ActionResult HandleError(int statusCode, string message)
        {
            return StatusCode(statusCode, new ErrorResponseDto
            {
                StatusCode = statusCode,
                Message = message
            });
        }        
    }
}
