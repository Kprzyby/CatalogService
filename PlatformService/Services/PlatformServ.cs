using PlatformService.Data;

namespace PlatformService.Services
{
    public class PlatformServ
    {
        #region Constructors

        public PlatformServ(PlatformRepo platformRepo)
        {
            _platformRepo = platformRepo;
        }

        #endregion Constructors

        #region Properties

        private readonly PlatformRepo _platformRepo;

        #endregion Properties
    }
}