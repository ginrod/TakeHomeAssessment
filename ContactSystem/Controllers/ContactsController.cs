using Asp.Versioning;
using ContactSystem.Application.Dtos;
using ContactSystem.Application.Entities;
using ContactSystem.Application.Services;
using ContactSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Granite.Controllers.Contacts
{
    [Route("api/v{version:apiVersion}/Contacts")]
    [ApiExplorerSettings(GroupName = "Contacts")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;


        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves a list of Contacts based on the provided search criteria.
        /// </summary>
        /// <param name="name">The name to search for Contacts.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of Contacts that match the search criteria.</returns>
        /// <response code="200">Contacts retrieved successfully.</response>
        /// <response code="400">If the search term is empty or invalid.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("getContacts")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContactEntity>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetContacts([FromQuery] string? officeId, [FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new ApiResponse<object>(false, "Search term cannot be empty.", null));
            }

            if (string.IsNullOrWhiteSpace(officeId))
            {
                _logger.LogWarning("Please select a valid office before searching for contacts.");

                return BadRequest(new ApiResponse<object>(false, "An office must be correctly selected", null));
            }

            if (!Guid.TryParse(officeId, out var officeGuid))
            {
                _logger.LogWarning("Invalid GUID format for OfficeId: {OfficeId}", officeId);

                return BadRequest(new ApiResponse<object>(false, "The selected office is not valid. Please try again.", null));
            }

            var (contacts, contactsCount) = await _contactService.SearchContactsAsync(officeGuid, name, page, pageSize);

            return Ok(new ApiResponse<IEnumerable<ContactEntity>>
                (success: true, "Contacts retrieved successfully", contacts, contactsCount)
            );
        }

        /// <summary>
        /// Retrieves a the list of Contacts.
        /// </summary>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of records per page.</param>
        /// <returns>A list of Contacts in the Office</returns>
        /// <response code="200">Contacts retrieved successfully.</response>
        /// <response code="500">If an internal server error occurs.</response>
        [HttpGet("getAllContacts")]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<ContactEntity>>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [ProducesResponseType(typeof(ApiResponse<object>), 500)]
        [Produces("application/json")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetAllContacts([FromQuery] string? officeId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (string.IsNullOrWhiteSpace(officeId))
            {
                _logger.LogWarning("Please select a valid office before searching for contacts.");

                return BadRequest(new ApiResponse<object>(false, "An office must be correctly selected", null));
            }

            if (!Guid.TryParse(officeId, out var officeGuid))
            {
                _logger.LogWarning("Invalid GUID format for OfficeId: {OfficeId}", officeId);

                return BadRequest(new ApiResponse<object>(false, "The selected office is not valid. Please try again.", null));
            }

            var (contacts, contactsCount) = await _contactService.SearchAllContactsInOfficeAsync(officeGuid, page, pageSize);

            return Ok(new ApiResponse<IEnumerable<ContactEntity>>
                (success: true, "Contacts retrieved successfully", contacts, contactsCount)
            );
        }
    }
}
