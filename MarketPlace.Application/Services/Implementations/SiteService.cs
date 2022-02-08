using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.Application.Utils;
using MarketPlace.DataLayer.Entities.Site;
using MarketPlace.DataLayer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Application.Services.Implementations
{
    public class SiteService : ISiteService
    {
        #region constructor

        private readonly IGenericRepository<SiteSetting> _siteSettingRepository;
        private readonly IGenericRepository<Slider> _sliderRepository;
        private readonly IGenericRepository<SiteBanner> _bannerRepository;

        public SiteService(IGenericRepository<SiteSetting> siteSettingRepository, IGenericRepository<Slider> sliderRepository, IGenericRepository<SiteBanner> bannerRepository)
        {
            _siteSettingRepository = siteSettingRepository;
            _sliderRepository = sliderRepository;
            _bannerRepository = bannerRepository;
        }

        #endregion

        #region site settings

        public async Task<SiteSetting> GetDefaultSiteSetting()
        {
            return await _siteSettingRepository.GetQuery().AsQueryable()
                .SingleOrDefaultAsync(s => s.IsDefault && !s.IsDelete);
        }

        #endregion

        #region slider

        public async Task<List<Slider>> GetAllActiveSliders()
        {
            return await _sliderRepository.GetQuery().AsQueryable()
                .Where(s => s.IsActive && s.IsDelete != true).ToListAsync();
        }     

        public async Task<Slider> GetSliderById(int sliderId)
        {
            return await _sliderRepository.GetQuery().AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == sliderId);
        }

        public async Task<bool> CreateSlider(Slider slider, IFormFile sliderImage)
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(sliderImage.FileName);
            var res = sliderImage.AddImageToServer(imageName, PathExtension.SliderOriginServer, 150, 150, PathExtension.SliderThumbImageServer);

            if (res)
            {
                // create product
                var newSlider = new Slider
                {
                    Link = slider.Link,
                    Description = slider.Description,
                    IsActive = true,
                    MainHeader = slider.MainHeader,
                    SecondHeader = slider.SecondHeader,
                    CreateDate = DateTime.Now,
                    IsDelete = false,
                    ImageName = imageName
                };

                await _sliderRepository.AddEntity(newSlider);
                await _sliderRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> EditSlider(Slider slider, IFormFile sliderImage)
        {
            var mainSlider = await _sliderRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Id == slider.Id);
            if (mainSlider == null) return false;
            mainSlider.Link = slider.Link;
            mainSlider.Description = slider.Description;
            mainSlider.IsActive = true;
            mainSlider.MainHeader = slider.MainHeader;
            mainSlider.SecondHeader = slider.SecondHeader;
            mainSlider.LastUpdateDate = DateTime.Now;

            if (sliderImage != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(sliderImage.FileName);

                var res = sliderImage.AddImageToServer(imageName, PathExtension.SliderOriginServer, 150, 150,
                    PathExtension.SliderThumbImageServer, mainSlider.ImageName);

                if (res)
                {
                    mainSlider.ImageName = imageName;
                }
            }
            _sliderRepository.EditEntity(mainSlider);
            await _sliderRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteSlider(int sliderId)
        {
            var mainSlider = await _sliderRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Id == sliderId);
            if (mainSlider == null) return false;

            _sliderRepository.DeleteEntity(mainSlider);
            await _sliderRepository.SaveChanges();
            return true;
        }



        #endregion

        #region site banners

        public async Task<List<SiteBanner>> GetAllActiveBanner()
        {
            return await _bannerRepository.GetQuery().AsQueryable().Where(b=>!b.IsDelete).ToListAsync();
        }

        public async Task<SiteBanner> GetBannerById(int bannerId)
        {
            return await _bannerRepository.GetQuery().AsQueryable()
                .FirstOrDefaultAsync(s => s.Id == bannerId);
        }

        public async Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements)
        {
            return await _bannerRepository.GetQuery().AsQueryable()
                .Where(s => placements.Contains(s.BannerPlacement)).ToListAsync();
        }

        public async Task<bool> CreateBanner(SiteBanner banner, IFormFile bannerImage)
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(bannerImage.FileName);
            var res = bannerImage.AddImageToServer(imageName, PathExtension.BannerOriginServer, 150, 150, PathExtension.BannerThumbImageServer);

            if (res)
            {
                // create product
                var newBanner = new SiteBanner
                {
                    Url = banner.Url,
                    ColSize = banner.ColSize,
                    CreateDate = DateTime.Now,
                    BannerPlacement = banner.BannerPlacement,
                    IsDelete = false,
                    ImageName = imageName
                };

                await _bannerRepository.AddEntity(newBanner);
                await _bannerRepository.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> EditBanner(SiteBanner banner, IFormFile bannerImage)
        {
            var mainBanner = await _bannerRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Id == banner.Id);
            if (mainBanner == null) return false;
            mainBanner.Url = banner.Url;
            mainBanner.ColSize = banner.ColSize;
            mainBanner.LastUpdateDate = DateTime.Now;
            mainBanner.BannerPlacement = banner.BannerPlacement;

            if (bannerImage != null)
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(bannerImage.FileName);

                var res = bannerImage.AddImageToServer(imageName, PathExtension.BannerOriginServer, 150, 150,
                    PathExtension.BannerThumbImageServer, mainBanner.ImageName);

                if (res)
                {
                    mainBanner.ImageName = imageName;
                }
            }
            _bannerRepository.EditEntity(mainBanner);
            await _bannerRepository.SaveChanges();
            return true;
        }

        public async Task<bool> DeleteBanner(int bannerId)
        {
            var mainBanner = await _bannerRepository.GetQuery().AsQueryable().SingleOrDefaultAsync(s => s.Id == bannerId);
            if (mainBanner == null) return false;

            _bannerRepository.DeleteEntity(mainBanner);
            await _bannerRepository.SaveChanges();
            return true;
        }

        #endregion

        #region dispose

        public async ValueTask DisposeAsync()
        {
            if (_siteSettingRepository != null) await _siteSettingRepository.DisposeAsync();
            if (_sliderRepository != null) await _sliderRepository.DisposeAsync();
            if (_bannerRepository != null) await _bannerRepository.DisposeAsync();
        }

        #endregion
    }
}
