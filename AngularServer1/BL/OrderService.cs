using AngularServer1.DAL;
using AngularServer1.Dto;
using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.BL
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDal _orderDal;
        public OrderService(IOrderDal orderDal)
        {
            this._orderDal = orderDal ?? throw new ArgumentNullException(nameof(orderDal));
        }
        public async Task<List<present>> AddOrder(string userId, int presentId)
        {
            return await _orderDal.AddOrderDal(userId, presentId);
        }
        public async Task<List<present>> UpdateOrder(string userId, int presentId)
        {
            return await _orderDal.UpdateOrderDal(userId, presentId);
        }
        public async Task<List<Order>> PayForOrder(string userId, List<int> order)
        {
            return await _orderDal.PayForOrderDal(userId,order);
        }
        public async Task<List<object>> GetAllpurchases()
        {
            return await _orderDal.GetAllpurchasesDal();
        }
        public async Task<List<User>> GetBuyersDetails(string userId)
        {
            return await _orderDal.GetBuyersDetailsDal();
        }

        public async Task<int> TotalIncome()
        {
            return await _orderDal.TotalIncomeDal();
        }

        public async Task<List<present>> getBuyerPresent(string userId)
        {
            return await _orderDal.getBuyerPresentDal(userId);
        }

        public async Task<List<present>> UpdateOrderOne(string userId, int presentId)
        {
            return await _orderDal.UpdateOrderOneDal(userId, presentId);
        }
    }

}
