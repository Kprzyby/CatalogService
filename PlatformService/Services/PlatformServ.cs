using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Data.DTOs;
using PlatformService.Models;

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
        # region Methods

        public async Task<ReadPlatformDTO> GetPlatformByIdAsync(int id)
        {
            try
            {
                Platform platform = await _platformRepo.GetPlatformByIdAsync(id);

                if (platform == null)
                {
                    return null;
                }

                ReadPlatformDTO dto = new ReadPlatformDTO()
                {
                    Id = platform.Id,
                    Name = platform.Name,
                    Publisher = platform.Publisher,
                    Cost = platform.Cost
                };

                return dto;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ReadPlatformDTO>> GetPlatformsAsync()
        {
            try
            {
                List<ReadPlatformDTO> platforms = await _platformRepo.
                    GetPlatforms()
                    .Select(e => new ReadPlatformDTO()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Publisher = e.Publisher,
                        Cost = e.Cost
                    })
                    .ToListAsync();

                return platforms;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ReadPlatformDTO> AddPlatformAsync(AddPlatformDTO dto)
        {
            try
            {
                Platform newPlatform = new Platform()
                {
                    Name = dto.Name,
                    Publisher = dto.Publisher,
                    Cost = dto.Cost,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                newPlatform = await _platformRepo.AddPlatformAsync(newPlatform);

                ReadPlatformDTO result = new ReadPlatformDTO()
                {
                    Id = newPlatform.Id,
                    Name = newPlatform.Name,
                    Publisher = newPlatform.Publisher,
                    Cost = newPlatform.Cost
                };

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePlatformAsync(UpdatePlatformDTO dto)
        {
            try
            {
                Platform oldPlatform = await _platformRepo.
                    GetPlatformByIdAsync(dto.Id);

                if (oldPlatform == null)
                {
                    return false;
                }

                oldPlatform.Name = dto.Name;
                oldPlatform.Publisher = dto.Publisher;
                oldPlatform.Cost = dto.Cost;
                oldPlatform.UpdatedDate = DateTime.Now;

                await _platformRepo.UpdatePlatformAsync(oldPlatform);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemovePlatformByIdAsync(int id)
        {
            try
            {
                Platform platform = await _platformRepo.
                        GetPlatformByIdAsync(id);

                if (platform == null)
                {
                    return false;
                }

                platform.DeletedDate = DateTime.Now;

                await _platformRepo.UpdatePlatformAsync(platform);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }
}