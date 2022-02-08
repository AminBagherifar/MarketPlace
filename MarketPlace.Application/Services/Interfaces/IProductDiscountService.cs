using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Discount;
using MarketPlace.DataLayer.DTOs.ProductDiscount;
using MarketPlace.DataLayer.Entities.Products;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IProductDiscountService : IAsyncDisposable
    {
        #region product discount

        Task<FilterProductDiscountDTO> FilterProductDiscount(FilterProductDiscountDTO filter);

        Task<CreateDiscountResult> CreateProductDiscount(CreateProductDiscountDto discount, long sellerId);

        #endregion
    }
}