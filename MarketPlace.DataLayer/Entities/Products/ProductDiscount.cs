﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MarketPlace.DataLayer.Entities.Common;

namespace MarketPlace.DataLayer.Entities.Products
{
    public class ProductDiscount : BaseEntity
    {
        #region properties

        public long ProductId { get; set; }

        [Range(0, 100)]
        public int Percentage { get; set; }

        public DateTime ExpireDate { get; set; }

        public int DiscountNumber { get; set; }

        #endregion

        #region relations

        public Product Product { get; set; }

        public ICollection<ProductDiscountUse> ProductDiscountUses { get; set; }

        #endregion
    }
}
