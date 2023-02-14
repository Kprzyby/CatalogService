using Microsoft.AspNetCore.Mvc;
using PlatformService.Services;

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
    }
}