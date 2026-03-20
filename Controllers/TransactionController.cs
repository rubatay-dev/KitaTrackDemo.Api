using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;
using KitaTrackDemo.Api.Common.Pagination;
using System.Security.Claims;

namespace KitaTrackDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _writeService;
        private readonly ITransactionQueryService _readService;

        public TransactionController(ITransactionService writeService, ITransactionQueryService readService)
        {
            _writeService = writeService;
            _readService = readService;   
        }

        // GET: api/transactions/id
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TransactionResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionResponseDto>> GetById(Guid id)
        {
            //throw new Exception("This is a test error");

            var userId = Guid.Parse(User.FindFirst((ClaimTypes.NameIdentifier))?.Value);

            var result = await _readService.GetByIdAsync(id, userId);

            if (!result.IsSuccess)
                return HandleError(StatusCodes.Status404NotFound, result.Error);

            return Ok(result.Value);
        }

        /// <summary>
        /// Retrieves a paginated list of transaction history based on filters.
        /// </summary>
        /// <param name="filter">The filter criteria (dates, type, etc.)</param>
        /// <returns>A paginated list of transactions.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<GetHistoryResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResponse<GetHistoryResponseDto>>> GetHistory([FromQuery] TransactionFilterDto filter)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _readService.GetHistoryAsync(filter, userId);

            return Ok(result.Value); 
        }

        // POST: api/transactions
        [HttpPost]
        [ProducesResponseType(typeof(TransactionResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionResponseDto>> Create([FromBody] CreateTransactionRequestDto request)
        {
            var userId = Guid.Parse(User.FindFirst((ClaimTypes.NameIdentifier))?.Value);

            var result = await _writeService.CreateAsync(request, userId);

            if (!result.IsSuccess)
                return HandleError(StatusCodes.Status400BadRequest, result.Error);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        // PUT: api/transactions
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]   
        public async Task<IActionResult> Edit([FromBody] EditTransactionRequestDto request)
        {
            var userId = Guid.Parse(User.FindFirst((ClaimTypes.NameIdentifier))?.Value);

            var result = await _writeService.EditAsync(request, userId);

            if (!result.IsSuccess)
                return HandleError(StatusCodes.Status400BadRequest, result.Error);
            
            return NoContent();
        }

        // DELETE: api/transactions
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst((ClaimTypes.NameIdentifier))?.Value);

            var result = await _writeService.DeleteAsync(id, userId);

            if (!result.IsSuccess)
                return HandleError(StatusCodes.Status400BadRequest, result.Error);

            return NoContent();
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
