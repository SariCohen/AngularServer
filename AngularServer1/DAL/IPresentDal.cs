using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AngularServer1.DAL
{
    public interface IPresentDal
    {
        Task<List<present>> GetAllPresent();
        Task<List<present>> AddPresentDal(present present);
        Task<List<present>> DeletePresentDal(int presentId);
        Task<List<present>> UpdatePresent(int presentId, present presentUp);
        Task<List<present>> GetPresentByName(string name);
        Task<List<present>> GetPresentByDonorDal(string donorName);
        Task<List<present>> GetPresentByPriceDal(int price);
        Task<List<present>> GetPresentByCategoryDal(List<string> category);
        Task<List<present>> GetPresentByExpensiveDal();
        Task<List<present>> GetPresentByCheapDal();
        
        Task<List<present>> GetByPurchasedDal();
    }
}
