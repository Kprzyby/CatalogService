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

        [HttpGet]
        [Route("Platform/GetPlatformAsync/{id}")]
        public async Task<IActionResult> GetPlatformAsync(int id)
        {
            ReadPlatformDTO result = await _platformService.GetPlatformByIdAsync(id);

            if (result == null)
            {
                return StatusCode(404, "Platform with that id doesn't exist");
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("Platform/GetPlatformsAsync")]
        public async Task<IActionResult> GetPlatformsAsync()
        {
            List<ReadPlatformDTO> result = await _platformService.GetPlatformsAsync();

            if (result == null)
            {
                return StatusCode(500, "Server error while executing the operation");
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Platform/AddPlatformAsync")]
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

        [HttpPut]
        [Route("Platform/UpdatePlatformAsync/{id}")]
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

        [HttpDelete]
        [Route("Platform/RemovePlatformAsync/{id}")]
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