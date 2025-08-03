using Asp.Versioning;
using ContactSystem.Application.Dtos;
using ContactSystem.Application.Entities;
using ContactSystem.Application.Services;
using ContactSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Granite.Controllers.Contacts
{
    [Route("api/v{version:apiVersion}/Offices")]
    [ApiExplorerSettings(GroupName = "Offices")]
    [ApiController]
    [ApiVersion("1.0")]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficesService _officeService;

        public OfficesController(IOfficesService officeService)
        {
            _officeService = officeService;
        }

        /// <summary>
        /// Retrieves a list of Offices.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of Offices</returns>
        /// <response code="200">Offices retrieved successfully.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("getOffices")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<OfficeEntity>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetOffices([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var offices = await _officeService.GetAllOfficesAsync();

            return Ok(new ApiResponse<IEnumerable<OfficeEntity>>
                (success: true, "Offices retrieved successfully", offices, offices.Count())
            );
        }
    }
}
