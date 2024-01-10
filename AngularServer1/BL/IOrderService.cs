using AngularServer1.Dto;
using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.BL
{
    public interface IOrderService
    {
        public Task<List<present>> AddOrder(string userId, int presentId);
        public Task<List<present>> UpdateOrder(string userId ,int presentId);
        public Task<List<present>> UpdateOrderOne(string userId, int presentId);
        public Task<List<Order>> PayForOrder(string userId,List<int> order);
        public Task<List<object>> GetAllpurchases();
        public Task<List<User>> GetBuyersDetails(string userId);
        public Task<int> TotalIncome();
        public Task<List<present>> getBuyerPresent(string userId);
        



    }
}
