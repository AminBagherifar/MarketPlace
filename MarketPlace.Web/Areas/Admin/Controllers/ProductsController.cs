using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Application.Services.Interfaces;
using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.Web.Http;
using MarketPlace.DataLayer.Entities.Products;

namespace MarketPlace.Web.Areas.Admin.Controllers
{
    public class ProductsController : AdminBaseController
    {
        #region constructor

        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #endregion

        #region filter products

        public async Task<IActionResult> Index(FilterProductDTO filter)
        {
            return View(await _productService.FilterProducts(filter));
        }

        #endregion

        #region accept product

        public async Task<IActionResult> AcceptSellerProduct(long id)
        {
            var result = await _productService.AcceptSellerProduct(id);
            if (result)
            {
                return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "محصول مورد نظر با موفقیت تایید شد", null);
            }

            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "محصول مورد نظر یافت نشد", null);
        }

        #endregion

        #region reject product

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectProduct(RejectItemDTO reject)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.RejectSellerProduct(reject);

                if (result)
                {

                    return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success,
                        "محصول مورد نظر با موفقیت رد شد", reject);
                }

                return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "اطلاعات مورد نظر جهت عدم تایید را به درستی وارد نمایید", null);
            }


            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Danger, "محصول مورد نظر یافت نشد", null);
        }

        #endregion

        #region Categories

        [Route("Admin/Categories")]
        public async Task<IActionResult> GetCategories()
        {
            return View(await _productService.GetAllActiveProductCategories());
        }

        [Route("Create-Category")]
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("Create-Category")]
        public async Task<IActionResult> CreateCategory(ProductCategory category)
        {
            if (!ModelState.IsValid) return View(category);
            await _productService.CreateCategory(category);
            return Redirect("/Admin/Categories");
        }

        [Route("Edit-Category/{categoryId}")]
        public async Task<IActionResult> EditCategory(int categoryId)
        {
            var category = await _productService.GetProductCategoryById(categoryId);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [Route("Edit-Category/{categoryId}")]
        public async Task<IActionResult> EditCategory(ProductCategory category)
        {
            if (!ModelState.IsValid) return View(category);
            await _productService.EditCategory(category);
            return Redirect("/Admin/Categories");
        }

        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _productService.GetProductCategoryById(categoryId);
            if (category == null) return NotFound();

            await _productService.DeleteCategory(category);

            return Redirect("/Admin/Categories");
        }

        #endregion
    }
}
