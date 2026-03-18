using KitaTrackDemo.Api.Common;
using KitaTrackDemo.Api.Interfaces;
using KitaTrackDemo.Api.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitaTrackDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ITransactionTypeService _service;

        public TransactionTypeController(ITransactionTypeService service)
        {
            _service = service; 
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<GetAllTransactionTypesResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetAllTransactionTypesResponse>>> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result.Value);
        }

        
    }
}
