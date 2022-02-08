using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.DataLayer.DTOs.Orders;
using MarketPlace.DataLayer.Entities.ProductOrder;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface IOrderService : IAsyncDisposable
    {
        #region order

        Task<long> AddOrderForUser(long userId);
        Task<Order> GetUserLatestOpenOrder(long userId);
        Task<int> GetTotalOrderPriceForPayment(long userId);
        Task PayOrderProductPriceToSeller(long userId, long refId);

        #endregion

        #region order detail

        Task ChangeOrderDetailCount(long detailId, long userId, int count);
        Task AddProductToOpenOrder(long userId, AddProductToOrderDTO order);
        Task<UserOpenOrderDTO> GetUserOpenOrderDetail(long userId);
        Task<bool> RemoveOrderDetail(long detailId, long userId);
        Task<List<Order>> GetOrders();
        Task<List<OrderDetail>> GetOrderDetails();

        #endregion
    }
}