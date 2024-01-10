using AngularServer1.DAL;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace AngularServer1.BL
{
    public interface IPresentService
    {
        public Task<List<present>> GetPresents();
        public Task<List<present>> AddPresent(present present);
        public Task<List<present>> DeletePresent(int presentId);
        public Task<List<present>> UpdatePresent(int presentId, present presentUp);
        public Task<List<present>> GetPresentByName(string name);
        public Task<List<present>> GetPresentByDonor(string donorName);
        public Task<List<present>> GetPresentByPrice(int price);
        public Task<List<present>> GetByExpensivePrice();
        public Task<List<present>> GetByCheapPrice();
        public Task<List<present>> GetByPurchased();
        
        public Task<List<present>> GetPresentByCategory(List<string> category);
        
    }
}
