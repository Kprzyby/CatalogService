using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.DTOs;
using PlatformService.Services;
using PlatformService.ViewModels;

namespace PlatformService.Controllers
{
    [ApiController]
    public class PlatformController : ControllerBase
    {
        #region Constructors

        public PlatformController(PlatformServ platformService)
        {
            _platformService = platformService;
        }

        #endregion Constructors

        #region Properties

        private readonly PlatformServ _platformService;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronous method for loading the platform specified by an id
        /// </summary>
        /// <param name="id">Id of the platform</param>
        /// <returns>A platform with a specified id</returns>
        /// <response code="200">Object containing information about the platform</response>
        /// <response code="404">Error message</response>
        [HttpGet]
        [Route("Platform/GetPlatformAsync/{id}")]
        [ProducesResponseType(typeof(ReadPlatformDTO), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetPlatformAsync(int id)
        {
            ReadPlatformDTO result = await _platformService.GetPlatformByIdAsync(id);

            if (result == null)
            {
                return StatusCode(404, "Platform with that id doesn't exist");
            }

            return Ok(result);
        }

        /// <summary>
        /// Asynchronous method for loading all platforms
        /// </summary>
        /// <returns>A list of objects containing information about each platform</returns>
        /// <response code="200">A list of objects containing information about each platform</response>
        /// <response code="500">Error message</response>
        [HttpGet]
        [Route("Platform/GetPlatformsAsync")]
        [ProducesResponseType(typeof(List<ReadPlatformDTO>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetPlatformsAsync()
        {
            List<ReadPlatformDTO> result = await _platformService.GetPlatformsAsync();

            if (result == null)
            {
                return StatusCode(500, "Server error while executing the operation");
            }

            return Ok(result);
        }

        /// <summary>
        /// Asynchronous method for adding a platform
        /// </summary>
        /// <param name="newPlatform">Object containing information about the new platform</param>
        /// <returns>String containing information about the result of the operation</returns>
        /// <response code="201">Success message</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("Platform/AddPlatformAsync")]
        [ProducesResponseType(typeof(string), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> AddPlatformAsync(CreatePlatformViewModel newPlatform)
        {
            AddPlatformDTO dto = new AddPlatformDTO()
            {
                Name = newPlatform.Name,
                Publisher = newPlatform.Publisher,
                Cost = newPlatform.Cost
            };

            bool result = await _platformService.AddPlatformAsync(dto);

            if (result == false)
            {
                return StatusCode(500, "Server error while executing the operation");
            }

            return StatusCode(201, "Platform created successfully");
        }

        /// <summary>
        /// Asynchronous method for updating the platform specified by an id
        /// </summary>
        /// <param name="updatedPlatform">Object containing new information about the platform</param>
        /// <param name="id">Id of the platform</param>
        /// <returns>Nothing if operation executes correctly or an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="500">Error message</response>
        [HttpPut]
        [Route("Platform/UpdatePlatformAsync/{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> UpdatePlatformAsync(UpdatePlatformViewModel updatedPlatform, int id)
        {
            UpdatePlatformDTO dto = new UpdatePlatformDTO()
            {
                Id = id,
                Name = updatedPlatform.Name,
                Publisher = updatedPlatform.Publisher,
                Cost = updatedPlatform.Cost
            };

            bool result = await _platformService.UpdatePlatformAsync(dto);

            if (result == false)
            {
                return StatusCode(500, "Server error while executing the operation");
            }

            return StatusCode(204);
        }

        /// <summary>
        /// Asynchronous method that removes the platform specified by an id
        /// </summary>
        /// <param name="id">Id of the platform</param>
        /// <returns>Nothing if operation executes correctly or an error message if it doesn't</returns>
        /// <response code="204"></response>
        /// <response code="500">Error message</response>
        [HttpDelete]
        [Route("Platform/RemovePlatformAsync/{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> RemovePlatformAsync(int id)
        {
            bool result = await _platformService.RemovePlatformByIdAsync(id);

            if (result == false)
            {
                return StatusCode(500, "Server error while executing the operation");
            }

            return StatusCode(204);
        }

        #endregion Methods
    }
}