using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Seller;
using MarketPlace.DataLayer.DTOs.SellerWallet;
using MarketPlace.DataLayer.Entities.Wallet;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISellerWalletService
    {
        #region wallet

        Task<FilterSellerWalletDTO> FilterSellerWallet(FilterSellerWalletDTO filter);
        Task AddWallet(SellerWallet wallet);

        #endregion
    }
}