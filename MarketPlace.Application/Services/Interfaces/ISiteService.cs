using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISiteService : IAsyncDisposable
    {
        #region site settings

        Task<SiteSetting> GetDefaultSiteSetting();

        #endregion

        #region slider

        Task<List<Slider>> GetAllActiveSliders();
        Task<Slider> GetSliderById(int sliderId);
        Task<bool> CreateSlider(Slider slider, IFormFile sliderImage);
        Task<bool> EditSlider(Slider slider, IFormFile sliderImage);
        Task<bool> DeleteSlider(int sliderId);

        #endregion

        #region site banners

        Task<List<SiteBanner>> GetAllActiveBanner();
        Task<SiteBanner> GetBannerById(int bannerId);
        Task<List<SiteBanner>> GetSiteBannersByPlacement(List<BannerPlacement> placements);
        Task<bool> CreateBanner(SiteBanner banner, IFormFile bannerImage);
        Task<bool> EditBanner(SiteBanner banner, IFormFile bannerImage);
        Task<bool> DeleteBanner(int bannerId);

        #endregion
    }
}