using Microsoft.EntityFrameworkCore;
using PlatformService.Data.Entities;

namespace PlatformService.Data
{
    public class DataContext : DbContext
    {
        #region Constructors

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        #endregion Constructors

        #region Properties

        public DbSet<Platform> Platforms { get; set; }

        #endregion Properties
    }
}