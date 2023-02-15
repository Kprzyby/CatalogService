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

        private async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Platform> GetPlatformByIdAsync(int id)
        {
            var platform = await _dataContext.Platforms
                .Where(e => !e.DeletedDate.HasValue)
                .SingleOrDefaultAsync(e => e.Id == id);

            return platform;
        }

        public IQueryable<Platform> GetPlatforms()
        {
            IQueryable<Platform> platforms = _dataContext.Platforms
                .Where(e => e.DeletedDate == null);

            return platforms;
        }

        public async Task<Platform> AddPlatformAsync(Platform platform)
        {
            await _dataContext.Platforms.AddAsync(platform);

            await SaveChangesAsync();

            return platform;
        }

        public async Task UpdatePlatformAsync(Platform platform)
        {
            _dataContext.Platforms.Update(platform);

            await SaveChangesAsync();
        }

        #endregion Methods
    }
}