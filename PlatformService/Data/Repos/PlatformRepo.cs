using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo
    {
        #region Constructors

        public PlatformRepo(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion Constructors

        #region Properties

        private readonly DataContext _dataContext;

        #endregion Properties

        #region Methods

        public async Task<Platform> GetPlatformById(int id)
        {
            var platform = await _dataContext.Platforms.SingleOrDefaultAsync(e => e.Id == id);

            return platform;
        }

        #endregion Methods
    }
}