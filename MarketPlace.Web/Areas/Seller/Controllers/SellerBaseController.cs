using MarketPlace.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MarketPlace.Web.Areas.Seller.Controllers
{
    [Authorize]
    [Area("Seller")]
    [Route("seller")]
    [CheckSellerState]
    public class SellerBaseController : Controller
    {
        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";
    }
}
