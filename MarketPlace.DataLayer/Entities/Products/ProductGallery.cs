using MarketPlace.DataLayer.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class ProductGallery : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        public int DisplayPriority { get; set; }

        [Display(Name = "نام تصویر")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string ImageName { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        #endregion
    }
}
