using AngularServer1.DAL;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AngularServer1.BL
{
    public class PresentService:IPresentService
    {
        private readonly IPresentDal _presentDal;
        
        public PresentService(IPresentDal presentDal)
        {
            this._presentDal= presentDal?? throw new ArgumentNullException(nameof(presentDal));
           
        }
        public async  Task<List<present>> GetPresents()
        {
            return await _presentDal.GetAllPresent();
        }
        public async Task<List<present>> AddPresent(present present)
        {
            return await _presentDal.AddPresentDal(present);
        }
        public async Task<List<present>> DeletePresent(int presentId)
        {
            return await _presentDal.DeletePresentDal(presentId);
        }
        public async Task<List<present>> UpdatePresent(int presentId, present presentUp)
        {
            return await _presentDal.UpdatePresent(presentId, presentUp);
        }

        public async Task<List<present>> GetPresentByName(string name)
        {
            return await _presentDal.GetPresentByName(name);
        }
        public async Task<List<present>> GetPresentByDonor(string donorName)
        {
            return await _presentDal.GetPresentByDonorDal(donorName);
        }
        public async Task<List<present>> GetPresentByPrice(int price)
        {
            return await _presentDal.GetPresentByPriceDal(price);
        }
       
        public async Task<List<present>> GetPresentByCategory(List<string> category)
        {
            return await _presentDal.GetPresentByCategoryDal(category);
        }
        public async Task<List<present>> GetByExpensivePrice()
        {
            return await _presentDal.GetPresentByExpensiveDal();
        }
        public async Task<List<present>> GetByCheapPrice()
        {
            return await _presentDal.GetPresentByCheapDal();
        }


        
        public async Task<List<present>> GetByPurchased()
        {
            return await _presentDal.GetByPurchasedDal();
        }
    }
}
