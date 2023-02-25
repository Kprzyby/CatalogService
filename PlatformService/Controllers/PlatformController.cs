﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("Platform/GetPlatformAsync/{id}", Name = "GetPlatformAsync")]
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
        /// <param name="filteringInfo">Object containing information about the paging, filtering and order</param>
        /// <remarks>
        /// The number of the first page is 1 and the minimal size of the page is also 1.
        ///
        /// The "SortInfo" parameter's keys should be "Name"/"Publiher"/"Cost" and their respective values - "asc" or "desc" depending on the desired sort order for this property.
        /// The KeyValuePairs should be added in the order you want the data to be sorted.
        /// If this parameter is not provided, the platforms will be sorted by name ascendingly.
        ///
        /// This method returns all platforms staring with the value of the "NameFilterValue" and/or "PublisherFilterValue" parameter (not case sensitive).
        /// </remarks>
        /// <returns>Object containing a list of platforms along with information about paging, filtering and order</returns>
        /// <response code="200">Object containing a list of platforms along with information about paging, filtering and order</response>
        /// <response code="500">Error message</response>
        [HttpGet]
        [Route("Platform/GetPlatformsAsync")]
        [ProducesResponseType(typeof(ReadPlatformsResponseDTO), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetPlatformsAsync(ReadPlatformsViewModel filteringInfo)
        {
            PlatformsFilteringDTO dto = new PlatformsFilteringDTO()
            {
                PageSize = filteringInfo.PageSize,
                PageNumber = filteringInfo.PageNumber,
                SortInfo = filteringInfo.SortInfo,
                NameFilterValue = filteringInfo.NameFilterValue,
                PublisherFilterValue = filteringInfo.PublisherFilterValue,
                MaxCostFilterValue = filteringInfo.MaxCostFilterValue
            };

            var result = await _platformService.GetPlatformsAsync(dto);

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
        /// <returns>New platform and a link to that resource</returns>
        /// <response code="201">Object containing information about the platform</response>
        /// <response code="500">Error message</response>
        [HttpPost]
        [Route("Platform/AddPlatformAsync")]
        [ProducesResponseType(typeof(ReadPlatformDTO), 201)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> AddPlatformAsync(CreatePlatformViewModel newPlatform)
        {
            AddPlatformDTO dto = new AddPlatformDTO()
            {
                Name = newPlatform.Name,
                Publisher = newPlatform.Publisher,
                Cost = newPlatform.Cost
            };

            ReadPlatformDTO result = await _platformService.AddPlatformAsync(dto);

            if (result == null)
            {
                return StatusCode(500, "Server error while executing the operation");
            }

            return CreatedAtRoute("GetPlatformAsync", new { id = result.Id }, result);
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