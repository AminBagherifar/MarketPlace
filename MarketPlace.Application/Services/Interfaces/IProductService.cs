using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Common;
using MarketPlace.DataLayer.DTOs.Products;
using MarketPlace.DataLayer.Entities.Products;
using Microsoft.AspNetCore.Http;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IProductService : IAsyncDisposable
    {
        #region products

        Task<FilterProductDTO> FilterProducts(FilterProductDTO filter);
        Task<CreateProductResult> CreateProduct(CreateProductDTO product, long sellerId, IFormFile productImage);
        Task<bool> AcceptSellerProduct(long productId);
        Task<bool> RejectSellerProduct(RejectItemDTO reject);
        Task<EditProductDTO> GetProductForEdit(long productId);
        Task<EditProductResult> EditSellerProduct(EditProductDTO product, long userId, IFormFile productImage);
        Task RemoveAllProductSelectedCategories(long productId);
        Task RemoveAllProductSelectedColors(long productId);
        Task AddProductSelectedColors(long productId, List<CreateProductColorDTO> colors);
        Task AddProductSelectedCategories(long productId, List<long> selectedCategories);
        Task<List<Product>> FilterProductsForSellerByProductName(long sellerId, string productName);
        Task<List<Product>> GetProducts(int take);
        Task<List<ProductDiscount>> GetAllOffProducts(int take);
        Task<List<Product>> GetPopularProducts(int take);

        #endregion

        #region product gallery

        Task<List<ProductGallery>> GetAllProductGalleries(long productId);
        Task<Product> GetProductBySellerOwnerId(long productId, long userId);
        Task<List<ProductGallery>> GetAllProductGalleriesInSellerPanel(long productId, long sellerId);
        Task<CreateOrEditProductGalleryResult> CreateProductGallery(CreateOrEditProductGalleryDTO gallery, long productId, long sellerId);
        Task<CreateOrEditProductGalleryDTO> GetProductGalleryForEdit(long galleryId, long sellerId);
        Task<CreateOrEditProductGalleryResult> EditProductGallery(long galleryId, long sellerId,
            CreateOrEditProductGalleryDTO gallery);
        Task<ProductDetailDTO> GetProductDetailById(long productId);

        #endregion

        #region product categories

        Task<List<ProductCategory>> GetAllProductCategoriesByParentId(long? parentId);
        Task<List<ProductCategory>> GetAllActiveProductCategories();
        Task<List<Product>> GetCategoryProductsByCategoryName(string categoryName, int count = 12);
        Task<ProductCategory> GetProductCategoryByUrlName(string productCategoryUrlName);
        Task<ProductCategory> GetProductCategoryById(int categoryId);
        Task CreateCategory(ProductCategory category);
        Task EditCategory(ProductCategory category);
        Task DeleteCategory(ProductCategory category);
        #endregion

        #region product feature

        Task CreateProductFeatures(long productId, List<CreateProductFeatureDTO> features);
        Task RemoveAllProductFeatures(long productId);

        #endregion
    }
}