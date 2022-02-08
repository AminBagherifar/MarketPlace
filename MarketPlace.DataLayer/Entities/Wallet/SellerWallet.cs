using System.ComponentModel.DataAnnotations;
using MarketPlace.DataLayer.Entities.Common;
using MarketPlace.DataLayer.Entities.Store;

namespace MarketPlace.DataLayer.Entities.Wallet
{
    public class SellerWallet : BaseEntity
    {
        #region properties

        public long SellerId { get; set; }

        public int Price { get; set; }

        public TransactionType TransactionType { get; set; }

        [Display(Name = "توضیحات")]
        [MaxLength(300, ErrorMessage = "{0} نمیتواند بیشتر از {1} کاراکتر باشد")]
        public string Description { get; set; }

        #endregion

        #region relations

        public Seller Seller { get; set; }

        #endregion
    }

    public enum TransactionType
    {
        [Display(Name = "واریز")]
        Deposit,
        [Display(Name = "برداشت")]
        Withdrawal
    }
}
