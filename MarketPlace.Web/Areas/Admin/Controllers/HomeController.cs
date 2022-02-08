using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.Entities.Site;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly IOrderService _orderService;
        private readonly ISiteService _siteService;
        public HomeController(IOrderService orderService, ISiteService siteService)
        {
            _orderService = orderService;
            _siteService = siteService;
        }
        #region index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Orders

        public async Task<IActionResult> Orders()
        {
            return View(await _orderService.GetOrders());
        }

        public async Task<IActionResult> OrderDetails()
        {
            return View(await _orderService.GetOrderDetails());
        }

        #endregion

        #region Slider

        [Route("Admin/Sliders")]
        public async Task<IActionResult> Sliders()
        {
            return View(await _siteService.GetAllActiveSliders());
        }

        public IActionResult CreateSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(Slider slider, IFormFile sliderImage)
        {
            if (sliderImage == null) TempData[WarningMessage] = "لطفا تصویر اسلایدر را وارد نمایید";
            await _siteService.CreateSlider(slider, sliderImage);
            return Redirect("/Admin/Sliders");
        }

        public async Task<IActionResult> EditSlider(int sliderId)
        {
            var slider = await _siteService.GetSliderById(sliderId);
            if (slider == null) return NotFound();
            return View(slider);
        }

        [HttpPost]
        public async Task<IActionResult> EditSlider(Slider slider, IFormFile sliderImage)
        {
            await _siteService.EditSlider(slider, sliderImage);
            return Redirect("/Admin/Sliders");
        }

        public async Task<IActionResult> DeleteSlider(int sliderId)
        {
            var res = await _siteService.DeleteSlider(sliderId);
            if (res == false)
                return NotFound();

            return Redirect("/Admin/Sliders");
        }

        #endregion

        #region Banners

        [Route("Admin/Banners")]
        public async Task<IActionResult> Banners()
        {
            return View(await _siteService.GetAllActiveBanner());
        }

        public IActionResult CreateBanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(SiteBanner banner, IFormFile bannerImage)
        {
            if (bannerImage == null) TempData[WarningMessage] = "لطفا تصویر را وارد نمایید";
            var res = await _siteService.CreateBanner(banner, bannerImage);
            return Redirect("/Admin/Banners");
        }

        public async Task<IActionResult> EditBanner(int bannerId)
        {
            var banner = await _siteService.GetBannerById(bannerId);
            if (banner == null) return NotFound();
            return View(banner);
        }

        [HttpPost]
        public async Task<IActionResult> EditBanner(SiteBanner banner, IFormFile bannerImage)
        {
            if (bannerImage == null) TempData[WarningMessage] = "لطفا تصویر را وارد نمایید";
            await _siteService.EditBanner(banner, bannerImage);
            return Redirect("/Admin/Banners");
        }

        public async Task<IActionResult> DeleteBanner(int bannerId)
        {
            var res = await _siteService.DeleteBanner(bannerId);
            if (res == false)
                return NotFound();

            return Redirect("/Admin/Banners");
        }

        #endregion
    }
}
