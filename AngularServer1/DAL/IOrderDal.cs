using ActiveUp.Net.Security.OpenPGP.Packets;
using AngularServer1.Dto;
using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.DAL
{
    public interface IOrderDal
    {
        Task<List<present>> AddOrderDal(string userId,int presentId);
        Task<List<present>> UpdateOrderDal(string userId, int presentId);
        Task<List<Order>>PayForOrderDal(string userId, List<int> order);
        Task<List<object>> GetAllpurchasesDal();
        Task<List<User>> GetBuyersDetailsDal();
        Task<int> TotalIncomeDal();
        Task<List<present>> getBuyerPresentDal(string userId);
        Task<List<present>> UpdateOrderOneDal(string userId, int presentId);
    }
}
