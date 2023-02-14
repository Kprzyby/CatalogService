using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using System.Linq;

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

        private async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Platform> GetPlatformByIdAsync(int id)
        {
            var platform = await _dataContext.Platforms
                .SingleOrDefaultAsync(e => e.Id == id)
                .ConfigureAwait(false);

            return platform;
        }

        public async Task<IEnumerable<Platform>> GetPlatformsAsync()
        {
            IEnumerable<Platform> platforms = await _dataContext.Platforms
                .ToListAsync()
                .ConfigureAwait(false);

            return platforms;
        }

        public async Task AddPlatformAsync(Platform platform)
        {
            await _dataContext.Platforms.
                AddAsync(platform)
                .ConfigureAwait(false);

            await SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdatePlatformAsync(Platform platform)
        {
            _dataContext.Platforms.Update(platform);

            await SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeletePlatformByIdAsync(Platform platform)
        {
            _dataContext.Platforms.Remove(platform);

            await SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion Methods
    }
}